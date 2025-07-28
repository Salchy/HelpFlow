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

        public Usuario GetUsuario(string idUsuario)
        {
            try
            {
                database.SetQuery("SELECT * FROM Usuarios WHERE Id = @Id");
                database.SetParameter("@Id", idUsuario);
                database.ExecQuery();
                if (!database.reader.Read())
                {
                    return null;
                }
                return new Usuario(
                        Convert.ToInt32(database.reader["Id"]),
                        database.reader["Nombre"].ToString(),
                        database.reader["Correo"].ToString(),
                        Convert.ToInt32(database.reader["TipoUsuario"])
                    );
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

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
                    database.reader["Nombre"].ToString(),
                    database.reader["Correo"].ToString(),
                    Convert.ToInt32(database.reader["TipoUsuario"])
                );
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public bool registrarUsuario(UsuarioDTO usuario, string password)
        {
            try
            {
                database.SetProcedure("SP_CrearUsuario");
                database.SetParameter("@userName", usuario.UserName);
                database.SetParameter("@name", usuario.Nombre);
                database.SetParameter("@email", usuario.Correo);
                database.SetParameter("@password", generateHashPassword(password));
                database.SetParameter("@TipoUsuario", usuario.TipoUsuario);

                database.ExecNonQuery();
                int newID = database.ExecScalar();
                usuario.Id = newID;
                return true;
            }
            catch (Exception Ex)
            {
                throw Ex;
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