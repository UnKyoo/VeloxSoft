using Npgsql;
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

        public List<Usuario> Ver_Usuarios(out String errorMessage)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    @"SELECT * FROM tbl_usuario WHERE estado = true", conn);
                using var reader = cmd.ExecuteReader();
                var lista_usuario = new List<Usuario>();
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

        public void Eliminar_Usuario(long id_Usuario, out string errorMessage) 
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    @"UPDATE tbl_usuario SET estado = false WHERE id_usuario = @id", conn);
                cmd.Parameters.AddWithValue("id", id_Usuario);
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

        public void Actualizar_Usuario(long id_Usuario, string Password, Roles Rol, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using var conn = new NpgsqlConnection(_dbConfig.GetConnection(Program.RolActual));
                conn.Open();
                using var cmd = new NpgsqlCommand(
                    @"UPDATE tbl_usuario SET password = @password, rol = @rol WHERE id_usuario = @id", conn);
                cmd.Parameters.AddWithValue("password", Password);
                cmd.Parameters.AddWithValue("rol", ObtenerRolEnum.ObtenerRolString(Rol));
                cmd.Parameters.AddWithValue("id", id_Usuario);
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

    }
}