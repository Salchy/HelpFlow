using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using DTO;

namespace AccesoDatos
{
    public class UsuarioDatos
    {
        private Database database;
        public UsuarioDatos()
        {
            database = new Database();
        }

        public static bool SesionActiva(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            if (usuario == null || usuario.Id == 0)
                return false;
            return true;
        }

        public static int GetLevel(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            if (usuario == null || usuario.Id == 0)
                return -1;
            return (int)usuario.TipoUsuario;
        }

        public static Usuario UsuarioActual(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            if (usuario == null || usuario.Id == 0)
                return null;
            return usuario;
        }

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public Usuario GetUsuario(int idUser)
        {
            try
            {
                database.SetQuery("SELECT * FROM Usuarios WHERE Id = @idUser");
                database.SetParameter("@idUser", idUser);
                database.ExecQuery();
                if (!database.reader.Read())
                {
                    return null;
                }
                return new Usuario(
                        Convert.ToInt32(database.reader["Id"]),
                        database.reader["UserName"].ToString(),
                        database.reader["Nombre"].ToString(),
                        database.reader["Correo"].ToString(),
                        Convert.ToInt32(database.reader["TipoUsuario"])
                    );
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                database.CloseConnection();
            }
        }

        /// <summary>
        /// Obtiene un usuario por su nombre de usuario.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Usuario GetUsuario(string userName)
        {
            try
            {
                database.SetQuery("SELECT * FROM Usuarios WHERE UserName = @userName");
                database.SetParameter("@userName", userName);
                database.ExecQuery();
                if (!database.reader.Read())
                {
                    return null;
                }
                return new Usuario(
                        Convert.ToInt32(database.reader["Id"]),
                        database.reader["UserName"].ToString(),
                        database.reader["Nombre"].ToString(),
                        database.reader["Correo"].ToString(),
                        Convert.ToInt32(database.reader["TipoUsuario"])
                    );
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                database.CloseConnection();
            }
        }

        /// <summary>
        /// Obtiene un usuario por su nombre de usuario y contraseña. (Útil para la validacion del login)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Usuario GetUsuario(string userName, string password)
        {
            try
            {
                database.SetQuery("SELECT * FROM Usuarios WHERE UserName = @userName");
                database.SetParameter("@userName", userName);
                database.ExecQuery();
                if (!database.reader.Read())
                {
                    return null;
                }
                if (database.reader["Clave"] == null || database.reader["Clave"].ToString() != generateHashPassword(password))
                {
                    return null;
                }
                return new Usuario(
                    Convert.ToInt32(database.reader["Id"]),
                    database.reader["UserName"].ToString(),
                    database.reader["Nombre"].ToString(),
                    database.reader["Correo"].ToString(),
                    Convert.ToInt32(database.reader["TipoUsuario"])
                );
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                database.CloseConnection();
            }
        }

        public List<UsuarioDTO> GetUsuarios()
        {
            List<UsuarioDTO> list = new List<UsuarioDTO>();
            try
            {
                database.SetQuery("SELECT Id, UserName, Nombre, Correo, TipoUsuario, IdEmpresa, Estado FROM Usuarios");
                database.ExecQuery();
                while (database.reader.Read())
                {
                    UsuarioDTO usuario = new UsuarioDTO(Convert.ToInt32(database.reader["Id"]), database.reader["UserName"].ToString(), database.reader["Nombre"].ToString(), database.reader["Correo"].ToString(), Convert.ToInt32(database.reader["TipoUsuario"]), Convert.ToInt32(database.reader["IdEmpresa"]), Convert.ToBoolean(database.reader["Estado"]));
                    list.Add(usuario);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                database.CloseConnection();
            }
            return list;
        }

        public bool registrarUsuario(UsuarioDTO usuario, string password)
        {
            try
            {
                database.SetProcedure("SP_CrearUsuario");
                database.SetParameter("@userName", usuario.UserName);
                database.SetParameter("@name", usuario.Nombre);
                database.SetParameter("@email", usuario.Correo);
                database.SetParameter("@idEmpresa", usuario.IdEmpresa);
                database.SetParameter("@password", generateHashPassword(password));
                database.SetParameter("@TipoUsuario", usuario.TipoUsuario);

                int newID = database.ExecScalar();
                usuario.Id = newID;
                return true;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                database.CloseConnection();
            }
        }

        public bool actualizarUsuario(UsuarioDTO usuario)
        {
            try
            {
                database.SetProcedure("SP_ModificarUsuario");
                database.SetParameter("@id", usuario.Id);
                database.SetParameter("@name", usuario.Nombre);
                database.SetParameter("@email", usuario.Correo);
                database.SetParameter("@idEmpresa", usuario.IdEmpresa);
                database.SetParameter("@tipoUsuario", usuario.TipoUsuario);
                database.ExecNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                database.CloseConnection();
            }
        }

        public bool updatePassword(int idUsuario, string password)
        {
            try
            {
                database.SetProcedure("SP_UpdatePassword");
                database.SetParameter("@idUsuario", idUsuario);
                database.SetParameter("@clave", generateHashPassword(password));
                database.ExecNonQuery();
                return true;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                database.CloseConnection();
            }
        }

        public bool emailInUse(string emailToFind)
        {
            try
            {
                database.SetQuery("SELECT COUNT(Correo) FROM Usuarios WHERE Correo = @correo;");
                database.SetParameter("@correo", emailToFind);
                database.ExecQuery();

                if (database.reader.Read())
                {
                    int count = Convert.ToInt32(database.reader[0]);
                    return count > 0;
                }
                return false;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                database.CloseConnection();
            }
        }

        public List<UsuarioDTO> GetSupporters()
        {
            List<UsuarioDTO> list = new List<UsuarioDTO>();
            try
            {
                database.SetQuery("SELECT Id, UserName, Nombre, Correo, TipoUsuario, IdEmpresa, Estado FROM Usuarios WHERE TipoUsuario = 0 AND Estado = 1");
                database.ExecQuery();
                while (database.reader.Read())
                {
                    UsuarioDTO usuario = new UsuarioDTO(Convert.ToInt32(database.reader["Id"]), database.reader["UserName"].ToString(), database.reader["Nombre"].ToString(), database.reader["Correo"].ToString(), Convert.ToInt32(database.reader["TipoUsuario"]), Convert.ToInt32(database.reader["IdEmpresa"]), Convert.ToBoolean(database.reader["Estado"]));
                    list.Add(usuario);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                database.CloseConnection();
            }
            return list;
        }

        public UsuarioDTO GetUsuarioDTO(int idUser)
        {
            try
            {
                database.SetQuery("SELECT UserName, Nombre, Correo FROM Usuarios WHERE Id = @idUser");

                database.SetParameter("@idUser", idUser);
                database.ExecQuery();

                if (!database.reader.Read())
                {
                    return null;
                }
                return new UsuarioDTO { UserName = database.reader["UserName"].ToString(), Nombre = database.reader["Nombre"].ToString() };
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                database.CloseConnection();
            }
        }

        private string generateHashPassword(string password)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = SHA256.Create().ComputeHash(inputBytes);

            // Formatear el hash en una cadena hexadecimal
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}