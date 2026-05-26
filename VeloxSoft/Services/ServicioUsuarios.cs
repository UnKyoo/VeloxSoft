using Konscious.Security.Cryptography;
using Npgsql;
using System.Security.Cryptography;
using System.Text;
using VeloxSoft.Config;
using VeloxSoft.Models;

namespace VeloxSoft.Services
{
    public class ServicioUsuarios
    {
        private readonly DatabaseConfig _dbConfig;

        public ServicioUsuarios(DatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public List<Usuario> Buscar_Usuarios(string id, string rol, string secion, string estado, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {

                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                string query = @"SELECT * FROM tbl_usuario WHERE 1=1";

                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;

                // ID
                if (!string.IsNullOrEmpty(id))
                {
                    long idUsuario = long.Parse(id);

                    query += " AND id = @id";
                    cmd.Parameters.AddWithValue("id", idUsuario);
                }
                else
                {
                    // ROL
                    if (!string.IsNullOrEmpty(rol))
                    {
                        string rolText = rol switch
                        {
                            "Gerente" => "0",
                            "Administrador" => "1",
                            "Cajero" => "2",
                            _ => "3"
                        };
                        query += " AND tipo = @rol::rol";
                        cmd.Parameters.AddWithValue("rol", rolText);
                    }

                    // SESION
                    if (!string.IsNullOrEmpty(secion))
                    {
                        bool secionBool = secion == "Conectado";

                        query += " AND secion = @secion";
                        cmd.Parameters.AddWithValue("secion", secionBool);
                    }

                    // ESTADO
                    if (string.IsNullOrEmpty(estado))
                    {
                        query += " AND estado = true";
                    }
                    else
                    {
                        bool estadoBool = estado == "Activo";

                        query += " AND estado = @estado";
                        cmd.Parameters.AddWithValue("estado", estadoBool);
                    }
                }
                cmd.CommandText = query;
                using var reader = cmd.ExecuteReader(); 
                var lista_usuario = new List<Usuario>();
                Console.WriteLine($"ROL: {rol}");
                Console.WriteLine($"SESION: {secion}");
                Console.WriteLine($"ESTADO: {estado}");
                foreach (NpgsqlParameter p in cmd.Parameters)
                {
                    Console.WriteLine($"{p.ParameterName} = {p.Value}");
                }
                while (reader.Read())
                {
                    lista_usuario.Add(new Usuario
                    {
                        Id = reader.GetInt64(0),
                        Nombre = reader.GetString(1),
                        Password = reader.GetString(2),
                        Rol = reader.GetString(3),
                        Secion = reader.GetBoolean(4),
                        Estado = reader.GetBoolean(5)
                    });

                }

                return lista_usuario;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                errorMessage = "Error Inesperado";
                return new List<Usuario>();
            }
        }

        public void Eliminar_Usuario(string id_Usuario, bool estado, out string errorMessage) 
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    @"UPDATE tbl_usuario SET estado = @estado WHERE id = @id", conn);
                long IdUsuario = long.Parse(id_Usuario);
                cmd.Parameters.AddWithValue("id", IdUsuario);
                cmd.Parameters.AddWithValue("estado", estado);
                int filasAfectadas = cmd.ExecuteNonQuery(); 
                if (filasAfectadas == 0)
                {
                    errorMessage = "No se encontró el Usuario con el ID proporcionado."; 
                }
            }
            catch (PostgresException e)
            {
                Console.Write(e.Message);
                errorMessage = "Error inesperado";
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                errorMessage = "Error inesperado";
            }
        }

        public void Actualizar_Usuario(string id_Cambio, string id_Actual, string Password, string RolText, bool Estado, out string errorMessage)
        {
            try
            {
                errorMessage = string.Empty;
                string hash = string.Empty;
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    "SELECT actualizar_usuario(@id, @id_c, @password, @rol::rol, @estado)", conn);
                long IdCambio = long.Parse(id_Cambio);
                long IdActual = long.Parse(id_Actual);
                if (string.IsNullOrEmpty(Password))
                {
                    using var cmdComp = new NpgsqlCommand(
                    "SELECT comp_usuario(@user)", conn);
                    cmdComp.Parameters.AddWithValue("@user", IdActual);

                    using var readerComp = cmdComp.ExecuteReader();

                    if (!readerComp.Read())
                    {
                        errorMessage = "Credenciales Invalidas";
                        return;
                    }

                    string resultComp = readerComp.GetString(0);
                    var partsComp = resultComp.Split('|');

                    if (partsComp[0] == "ERROR")
                    {
                        errorMessage = partsComp[1];
                        return;
                    }

                   hash = partsComp[1];
                }
                else
                {
                    hash = HashPassword(Password);
                }
                cmd.Parameters.AddWithValue("id", IdActual);
                cmd.Parameters.AddWithValue("id_c", IdCambio);
                cmd.Parameters.AddWithValue("password", hash);
                cmd.Parameters.AddWithValue("rol", RolText);
                cmd.Parameters.AddWithValue("estado", Estado);
                using var reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    errorMessage = "Error Inesperado 1";
                    return;
                }

                string result = reader.GetString(0);
                var parts = result.Split('|');

                if (parts[0] == "ERROR")
                {
                    errorMessage = parts[1];
                }
            }
            catch (PostgresException e)
            {
                Console.Write(e.Message);
                errorMessage = $"Error inesperado: {e.Message}";
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                errorMessage = "Error inesperado";
            }
        }

        public void Insertar_Usuario(string id_Usuario, string Nombre, string rolText, string Password, out string errorMessage)
        {
            try
            {
                errorMessage = string.Empty;
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();
                using var cmd = new NpgsqlCommand("SELECT insertar_usuario(@user, @nombre, @password, @rol::rol, @estado)", conn);

                long userId = long.Parse(id_Usuario);
                cmd.Parameters.AddWithValue("@user", userId);
                cmd.Parameters.AddWithValue("@nombre", Nombre);
                cmd.Parameters.AddWithValue("@password", HashPassword(Password));
                cmd.Parameters.AddWithValue("@rol", rolText);
                cmd.Parameters.AddWithValue("@estado", true);

                using var reader = cmd.ExecuteReader();

                if (!reader.Read())
                {
                    errorMessage = "Error Inesperado";
                    return;
                }

                string result = reader.GetString(0);
                var parts = result.Split('|');

                if (parts[0] == "ERROR")
                {
                    errorMessage = parts[1];
                }
            }
            catch (PostgresException e)
            {
                Console.Write(e.Message);
                errorMessage = "Error inesperado";
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                errorMessage = "Error inesperado";
            }
        }

        public void CerrarSesion(out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Roles.Crud));
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    "UPDATE tbl_usuario SET secion = false WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("id", Program.UsuarioLogueado.Id);
                cmd.ExecuteNonQuery();
            }
            catch (PostgresException e)
            {
                errorMessage = "Error inesperado porfavor intente nuevamente";
            }
        }

        public void ActualizarActividad(long idUsuario, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();

                using var cmd = new NpgsqlCommand(
                    "UPDATE tbl_usuario SET ultima_actividad = @timestamp WHERE id = @id", conn);
                cmd.Parameters.AddWithValue("timestamp", DateTime.Now);
                cmd.Parameters.AddWithValue("id", idUsuario);

                cmd.ExecuteNonQuery();
            }
            catch (PostgresException e)
            {
                errorMessage = "Error de base de datos.";
            }
            catch (Exception e)
            {
                errorMessage = $"Error inesperado: {e.Message}";
            }
        }

        public static string HashPassword(string password)
        {
            // Generar salt aleatorio
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = 8, // núcleos
                Iterations = 4,
                MemorySize = 1024 * 64 // 64 MB
            };

            byte[] hash = argon2.GetBytes(32);

            // Combinar salt + hash
            byte[] result = new byte[salt.Length + hash.Length];
            Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
            Buffer.BlockCopy(hash, 0, result, salt.Length, hash.Length);

            return Convert.ToBase64String(result);
        }

    }
}