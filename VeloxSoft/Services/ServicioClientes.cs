using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using VeloxSoft.Config;
using VeloxSoft.Models; 

namespace VeloxSoft.Services
{
    public class ServicioClientes
    {
        private readonly DatabaseConfig _dbConfig;

        public ServicioClientes(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public List<Cliente> Ver_Clientes(out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                using var cmd = new NpgsqlCommand(@"
                    SELECT 
                        c.id_cel,
                        c.nombre,
                        c.apellido,
                        c.apodo,
                        d.num_casa || ' ' || d.calle || ' x ' || d.cruzamientos || ', ' || col.colonia AS direccion
                    FROM tbl_cliente c
                    INNER JOIN tbl_direccion d ON c.direccion_c = d.id_direc
                    INNER JOIN tbl_colonia col ON d.colonia_d = col.idcolonia", conn);
                using var reader = cmd.ExecuteReader();
                var lista = new List<Cliente>();

                while (reader.Read())
                {
                    lista.Add(new Cliente
                    {
                        IdCliente = reader.GetInt64(0),
                        Nombre = reader.GetString(1),
                        Apellido = reader.GetString(2),
                        Apodo = reader.GetString(3),
                        Direccion = reader.GetString(4)
                    });
                }

                return lista;
            }

            //Enviamos errores generales puesto que aqui no existe una funcion de la BD que de error especifico, si se llegara a implementar una funcion de la BD que pueda dar error especifico, se puede modificar este catch para enviar el error especifico
            catch (PostgresException e)
            {
                errorMessage = "Error inesperado PG.";
                return new List<Cliente>();
            }
            catch (Exception e)
            {
                errorMessage = "Error inesperado G.";
                return new List<Cliente>();
            }
        }

        //función para rellnar el combo de direcciones al insertar un cliente, se muestra la dirección completa para facilitar la selección al usuario
        public List<DireccionItem> Ver_Direcciones(out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                using var cmd = new NpgsqlCommand(@"
            SELECT 
                d.id_direc,
                d.num_casa || ' ' || d.calle || ' x ' || d.cruzamientos || ', ' || col.colonia AS direccion
            FROM tbl_direccion d
            INNER JOIN tbl_colonia col ON d.colonia_d = col.idcolonia", conn);

                using var reader = cmd.ExecuteReader();
                var lista = new List<DireccionItem>();

                while (reader.Read())
                {
                    lista.Add(new DireccionItem
                    {
                        Id = reader.GetInt32(0),
                        Texto = reader.GetString(1)
                    });
                }

                return lista;
            }
            //Enviamos errores generales puesto que aqui no existe una funcion de la BD que de error especifico, si se llegara a implementar una funcion de la BD que pueda dar error especifico, se puede modificar este catch para enviar el error especifico
            catch (PostgresException e)
            {
                errorMessage = "Error inesperado PG.";
                return new List<DireccionItem>();
            }
            catch (Exception e)
            {
                errorMessage = "Error inesperado G.";
                return new List<DireccionItem>();
            }
        }

        //función para rellenar el combo de colonias al insertar una dirección, se muestra solo el nombre de la colonia para facilitar la selección al usuario
        public List<ColoniaItem> Ver_Colonias(out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "SELECT idcolonia, colonia FROM tbl_colonia ORDER BY colonia", conn);

                using var reader = cmd.ExecuteReader();
                var lista = new List<ColoniaItem>();

                while (reader.Read())
                {
                    lista.Add(new ColoniaItem
                    {
                        Id = reader.GetInt32(0),
                        Texto = reader.GetString(1)
                    });
                }

                return lista;
            }
            catch (PostgresException e)
            {
                errorMessage = "Error inesperado PG";
                return new List<ColoniaItem>();
            }
            catch (Exception e)
            {
                errorMessage = "Error inesperado G.";
                return new List<ColoniaItem>();
            }
        }

        //Función para insertar una colonia, se muestra el mensaje de error específico que devuelve la función de la BD en caso de que ocurra un error, si no ocurre ningún error se devuelve el mensaje de éxito
        public string Insertar_Colonia(string colonia, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "SELECT insertar_colonia(@colonia)", conn);
                cmd.Parameters.AddWithValue("colonia", colonia);

                string resultado = cmd.ExecuteScalar().ToString();
                var parts = resultado.Split('|');

                if (parts[0] == "ERROR")
                    errorMessage = parts[1];

                return parts[1];
            }
            catch (PostgresException e)
            {
                return errorMessage = "Error inesperado PG.";
            }
            catch (Exception e)
            {
                return errorMessage = "Error inesperado G.";
            }
        }
        public string Insertar_Direccion(string numCasa, string calle, string cruzamientos, string referencia, int idColonia, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "SELECT insertar_direccion(@numCasa, @calle, @cruzamientos, @referencia, @idColonia)", conn);
                cmd.Parameters.AddWithValue("numCasa", numCasa);
                cmd.Parameters.AddWithValue("calle", calle);
                cmd.Parameters.AddWithValue("cruzamientos", cruzamientos);
                cmd.Parameters.AddWithValue("referencia", referencia);
                cmd.Parameters.AddWithValue("idColonia", idColonia);

                string resultado = cmd.ExecuteScalar().ToString();
                var parts = resultado.Split('|');

                if (parts[0] == "ERROR")
                    errorMessage = parts[1];

                return parts[1];
            }
            catch (PostgresException e)
            {
                return errorMessage = "Error de base de datos.";
            }
            catch (Exception e)
            {
                return errorMessage = "Error inesperado.";
            }
        }
    }
}

    

