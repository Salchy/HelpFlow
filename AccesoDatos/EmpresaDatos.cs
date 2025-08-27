using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace AccesoDatos
{
    public class EmpresaDatos
    {
        Database Database = new Database();

        public List<Empresa> GetEmpresas()
        {
            List<Empresa> lista = new List<Empresa>();

            try
            {
                Database.SetQuery("SELECT Id, Nombre FROM Empresas ORDER BY Id ASC;");
                Database.ExecQuery();

                while (Database.reader.Read())
                {
                    Empresa empresa = new Empresa();
                    empresa.Id = Convert.ToInt32(Database.reader["Id"]);
                    empresa.Nombre = Database.reader["Nombre"].ToString();
                    lista.Add(empresa);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Database.CloseConnection();
            }
        }

    }
}
