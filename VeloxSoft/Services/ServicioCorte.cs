using Npgsql;
using System;
using System.Collections.Generic;
using VeloxSoft.Config;
using VeloxSoft.Models;

namespace VeloxSoft.Services
{
    public class ServicioCorte
    {
        private readonly DatabaseConfig _dbConfig;

        public ServicioCorte(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public string Realizar_Corte(out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                using var cmd = new NpgsqlCommand("SELECT realizar_corte()", conn);

                string resultado = cmd.ExecuteScalar().ToString();
                var parts = resultado.Split('|');

                if (parts[0] == "ERROR")
                    errorMessage = parts[1];

                return parts[1];
            }
            catch (PostgresException)
            {
                return errorMessage = "Error inesperado PG.";
            }
            catch (Exception)
            {
                return errorMessage = "Error inesperado G.";
            }
        }
    
        public List<Corte> Ver_Cortes(out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "SELECT id_corte, fecha, total_ventas, total_gasto, ganancia FROM tbl_corte ORDER BY fecha DESC", conn);

                using var reader = cmd.ExecuteReader();
                var lista = new List<Corte>();

                while (reader.Read())
                {
                    lista.Add(new Corte
                    {
                        IdCorte = reader.GetInt64(0),
                        Fecha = reader.GetDateTime(1).ToString("dd/MM/yyyy"),
                        TotalVentas = reader.GetDecimal(2),
                        TotalGasto = reader.GetDecimal(3),
                        Ganancia = reader.GetDecimal(4)
                    });
                }

                return lista;
            }
            catch (PostgresException)
            {
                errorMessage = "Error inesperado PG.";
                return new List<Corte>();
            }
            catch (Exception)
            {
                errorMessage = "Error inesperado G.";
                return new List<Corte>();
            }
        }
    }
}