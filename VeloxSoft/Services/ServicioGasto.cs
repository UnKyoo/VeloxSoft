using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using VeloxSoft.Config;
using VeloxSoft.Models;

namespace VeloxSoft.Services
{
    public class ServicioGasto
    {
        private readonly DatabaseConfig _dbConfig;
        public ServicioGasto(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public bool NombreGasto(long id_gasto, string descripcion, out string erroresMessage)
        {
            erroresMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(descripcion))
            {
                erroresMessage = "La descripción no puede estar vacía.";
                return false;
            }
            if (descripcion.Contains('=') || descripcion.Contains(';'))
            {
                erroresMessage = "La descripción contiene caracteres no permitidos (= ;)";
                return false;
            }
            if (descripcion.Length > 50)
            {
                erroresMessage = "La descripción no puede superar 50 caracteres.";
                return false;
            }

            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                string sql = "SELECT agregar_gasto(@descripcion)";

                using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@descripcion", descripcion);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                erroresMessage = $"Error inesperado: {ex.Message}";
                return false;
            }
        }

        public bool AgregarDetalleGasto(decimal monto, long id_gasto, out string erroresMessage)
        {
            erroresMessage = string.Empty;

            if (monto <= 0)
            {
                erroresMessage = "El monto debe ser mayor a 0.";
                return false;
            }

            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                string sql = "SELECT agregar_det_gasto(@monto, @id_gasto)";

                using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@monto", monto);
                cmd.Parameters.AddWithValue("@id_gasto", id_gasto);
                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                erroresMessage = $"Error inesperado: {ex.Message}";
                return false;
            }
        }

        public List<DetalleGasto> Buscar_DetalleGastos(string fechaInicio, string fechaFin, string proveedor, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
              
                conn.Open();

                string query = @"SELECT d.id_gasto_d, d.monto, d.fecha, d.id_gasto, d.id_corte, g.descripcion
                    FROM tbl_det_gasto d
                    INNER JOIN tbl_gasto g ON g.id_gasto = d.id_gasto
                    WHERE d.fecha >= @fechaInicio AND d.fecha <= @fechaFin";

                if (proveedor != "Todos")
                    query += " AND g.descripcion = @proveedor";

                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("fechaInicio", DateOnly.Parse(fechaInicio));
                cmd.Parameters.AddWithValue("fechaFin", DateOnly.Parse(fechaFin));

                if (proveedor != "Todos")
                    cmd.Parameters.AddWithValue("proveedor", proveedor);

                
                using var reader = cmd.ExecuteReader();
                var lista = new List<DetalleGasto>();

                while (reader.Read())
                {
                    lista.Add(new DetalleGasto
                    {
                        IdGastoD = reader.GetInt64(0),
                        Monto = reader.GetDecimal(1),
                        Fecha = reader.GetFieldValue<DateOnly>(2),
                        IdGasto = reader.GetInt64(3),
                        IdCorte = reader.IsDBNull(4) ? null : reader.GetInt64(4),
                        Descripcion = reader.GetString(5) // ← nombre del proveedor
                    });
                }
                return lista;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                errorMessage = e.Message;
                return new List<DetalleGasto>();
            }
        }

        public List<Gasto> Buscar_Gastos(out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                string query = "SELECT id_gasto, descripcion FROM tbl_gasto";
                using var cmd = new NpgsqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                var lista = new List<Gasto>();
                while (reader.Read())
                {
                    lista.Add(new Gasto
                    {
                        IdGasto = reader.GetInt64(0),
                        Descripcion = reader.GetString(1)
                    });
                }
                return lista;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                errorMessage = "Error Inesperado";
                return new List<Gasto>();
            }
        }
















    }
 }
