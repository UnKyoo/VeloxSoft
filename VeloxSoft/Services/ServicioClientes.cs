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

        //Ver_clientes Filtra y si no muestra direcciones 
        public List<Cliente> Ver_Clientes(out string errorMessage, string id = null, string colonia = null)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();
                //Consulta base que une cliente, dirección y colonia para mostrar la información completa del cliente y es estructurada
                string query = @"
                SELECT 
                    c.id_cel,
                    c.nombre,
                    c.apellido,
                    c.apodo,
                    d.num_casa || ' ' || d.calle || ' x ' || d.cruzamientos || ', ' || col.colonia AS direccion
                FROM tbl_cliente c
                INNER JOIN tbl_direccion d ON c.direccion_c = d.id_direc
                INNER JOIN tbl_colonia col ON d.colonia_d = col.idcolonia
                WHERE 1=1";

                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;

                // Si hay ID busca solo por ID, ignora colonia
                // Si no hay ID pero hay colonia, busca por colonia
                //Creacion de consultas estructuradas para filtrar por ID o por colonia
                if (!string.IsNullOrEmpty(id))
                {
                    query += " AND c.id_cel = @id";
                    cmd.Parameters.AddWithValue("id", long.Parse(id));
                }
                else if (!string.IsNullOrEmpty(colonia))
                {
                    query += " AND col.colonia = @colonia";
                    cmd.Parameters.AddWithValue("colonia", colonia);
                }

                cmd.CommandText = query;

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

        //función para rellenar el combo de colonias al insertar una dirección, se muestra solo el nombre de la colonia para facilitar la selección al usuario
        public List<Colonia> Ver_Colonias(out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "SELECT idcolonia, colonia FROM tbl_colonia ORDER BY colonia", conn);

                using var reader = cmd.ExecuteReader();
                var lista = new List<Colonia>();

                while (reader.Read())
                {
                    lista.Add(new Colonia
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
                return new List<Colonia>();
            }
            catch (Exception e)
            {
                errorMessage = "Error inesperado G.";
                return new List<Colonia>();
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

        public string Insertar_Cliente(long idCel, string nombre, string apellido, string apodo, int idDireccion, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "SELECT insertar_cliente(@idCel, @nombre, @apellido, @apodo, @idDireccion)", conn);
                cmd.Parameters.AddWithValue("idCel", idCel);
                cmd.Parameters.AddWithValue("nombre", nombre);
                cmd.Parameters.AddWithValue("apellido", apellido);
                cmd.Parameters.AddWithValue("apodo", apodo);
                cmd.Parameters.AddWithValue("idDireccion", idDireccion);

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
        //Ver_Direcciones Permite cargar direcciones en la tabla y enseñarlas dentro del combobox de direcciones
        // Ver_Direcciones Permite cargar direcciones en la tabla filtrando por calle/ID y colonia
        public List<Direccion> Ver_Direcciones(out string errorMessage, string id = null, string colonia = null)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                // 1. Consulta base original
                string query = @"
                SELECT d.id_direc, d.num_casa, d.calle, d.cruzamientos, d.referencia, col.colonia
                FROM tbl_direccion d
                INNER JOIN tbl_colonia col ON d.colonia_d = col.idcolonia
                WHERE 1=1"; // Agregamos el 1=1 para meter filtros dinámicos

                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;

                // 2. Filtro por cuadro de texto (Busca por ID numérico de dirección o por nombre de calle)
                //
                if (!string.IsNullOrEmpty(id))
                {
                    if (long.TryParse(id, out long idDir))
                    {
                        query += " AND d.id_direc = @idDir";
                        cmd.Parameters.AddWithValue("idDir", (int)idDir);
                    }
                    else
                    {
                        query += " AND d.calle ILIKE @calle";
                        cmd.Parameters.AddWithValue("calle", $"%{id}%");
                    }
                }

                // 3. Filtro por Colonia (proveniente del ComboBox de la UI)
                if (!string.IsNullOrEmpty(colonia))
                {
                    query += " AND col.colonia = @colonia";
                    cmd.Parameters.AddWithValue("colonia", colonia);
                }

                cmd.CommandText = query;

                using var reader = cmd.ExecuteReader();
                var lista = new List<Direccion>();

                while (reader.Read())
                {
                    lista.Add(new Direccion
                    {
                        Id = reader.GetInt32(0),
                        NumCasa = reader.GetString(1),
                        Calle = reader.GetString(2),
                        Cruzamientos = reader.GetString(3),
                        Referencia = reader.GetString(4),
                        Colonia = reader.GetString(5)
                    });
                }

                return lista;
            }
            catch (PostgresException e)
            {
                errorMessage = "Error inesperado PG.";
                return new List<Direccion>();
            }
            catch (Exception e)
            {
                errorMessage = "Error inesperado G.";
                return new List<Direccion>();
            }
        }
    }
}

    

