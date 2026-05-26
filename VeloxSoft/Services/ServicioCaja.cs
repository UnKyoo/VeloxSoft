using Npgsql;
using System;
using System.Collections.Generic;
using VeloxSoft.Config;
using VeloxSoft.Models;

namespace VeloxSoft.Services
{
    public class ServicioCaja
    {
        private readonly DatabaseConfig _dbConfig;

        public ServicioCaja(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public List<Producto> BuscarProductos(string filtro, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                using var cmd = new NpgsqlCommand(@"
                    SELECT id_producto, nombre, cantidad, precio, id_categoria
                    FROM tbl_producto
                    WHERE estado = true
                      AND cantidad > 0
                      AND (
                          CAST(id_producto AS TEXT) ILIKE @filtro
                          OR nombre ILIKE @filtro
                      )
                    ORDER BY nombre
                    LIMIT 10
                ", conn);

                cmd.Parameters.AddWithValue("filtro", $"%{filtro}%");

                using var reader = cmd.ExecuteReader();
                var lista = new List<Producto>();

                while (reader.Read())
                {
                    lista.Add(new Producto
                    {
                        IdProducto = reader.GetString(0),
                        Nombre = reader.GetString(1),
                        Cantidad = reader.GetDecimal(2),
                        Precio = reader.GetDecimal(3),
                        IdCategoria = reader.GetString(4)
                    });
                }

                return lista;
            }
            catch (PostgresException)
            {
                errorMessage = "Error inesperado PG.";
                return new List<Producto>();
            }
            catch (Exception)
            {
                errorMessage = "Error inesperado G.";
                return new List<Producto>();
            }
        }

        public List<Usuario> BuscarUsuarios(string filtro, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                using var cmd = new NpgsqlCommand(@"
                SELECT id, nombre, tipo
                FROM tbl_usuario
                WHERE secion = true
                  AND estado = true
                  AND (
                      nombre ILIKE @filtro
                      OR CAST(id AS TEXT) ILIKE @filtro
                  )
                ORDER BY nombre
                LIMIT 10
                ", conn);

                cmd.Parameters.AddWithValue("filtro", $"%{filtro}%");

                using var reader = cmd.ExecuteReader();
                var lista = new List<Usuario>();

                while (reader.Read())
                {
                    lista.Add(new Usuario
                    {
                        Id = reader.GetInt64(0),
                        Nombre = reader.GetString(1),
                        Rol = reader.GetString(2)
                    });
                }

                return lista;
            }
            catch (PostgresException)
            {
                errorMessage = "Error inesperado PG.";
                return new List<Usuario>();
            }
            catch (Exception)
            {
                errorMessage = "Error inesperado G.";
                return new List<Usuario>();
            }
        }

        public List<(string Id, string Tipo)> Ver_MetodosPago(out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "SELECT id_pago, tipo_pago FROM tbl_pago ORDER BY tipo_pago", conn);

                using var reader = cmd.ExecuteReader();
                var lista = new List<(string, string)>();

                while (reader.Read())
                    lista.Add((reader.GetString(0), reader.GetString(1)));

                return lista;
            }
            catch (PostgresException)
            {
                errorMessage = "Error inesperado PG.";
                return new List<(string, string)>();
            }
            catch (Exception)
            {
                errorMessage = "Error inesperado G.";
                return new List<(string, string)>();
            }
        }
    }
}