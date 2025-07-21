using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AccesoDatos
{
    public class Database
    {
        private SqlConnection connection;
        private SqlCommand command;
        public SqlDataReader reader;
        public Database()
        {
            try
            {
                connection = new SqlConnection("server =.\\SQLEXPRESS; database = HelpFlow; integrated security = true");
                command = new SqlCommand();
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Setea la consulta a ejecutar en la base de datos.
        /// </summary>
        /// <param name="queryString"></param>
        public void SetQuery(string queryString)
        {
            command.Parameters.Clear();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = queryString;
        }

        /// <summary>
        /// Setea el procedimiento almacenado para ejecutar en la base de datos.
        /// </summary>
        /// <param name="procedureString"></param>
        public void SetProcedure(string procedureString)
        {
            command.Parameters.Clear();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = procedureString;
        }

        /// <summary>
        /// Agrega un parámetro a la consulta o procedimiento almacenado.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetParameter(string key, object value)
        {
            command.Parameters.AddWithValue(key, value);
        }

        /// <summary>
        /// Para consultas de tipo SELECT
        /// </summary>
        public void ExecQuery()
        {
            command.Connection = connection;
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Para consultas de tipo INSERT, UPDATE o DELETE
        /// </summary>
        public void ExecNonQuery()
        {
            command.Connection = connection;
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Para ejecutar consultas de tipo SCALAR (Retorna un valor entero), Funciones de resumen de SQL
        /// </summary>
        /// <returns>Retorna valores enteros de las funciones de resumen de SQL</returns>
        public int ExecScalar()
        {
            command.Connection = connection;
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result == null || result == DBNull.Value)
                {
                    return 0;
                }
                return int.Parse(result.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void CloseConnection()
        {
            if (reader != null)
                reader.Close();
            connection.Close();
        }
    }
}