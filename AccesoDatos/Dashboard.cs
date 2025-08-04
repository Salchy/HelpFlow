using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace AccesoDatos
{
    public class Dashboard
    {
        private Database database;
        public Dashboard()
        {
            database = new Database();
        }

        public List<DashboardDTO> GetTicketsCount(int idUsuario = 0)
        {
            List<DashboardDTO> ticketsCount = new List<DashboardDTO>();
            try
            {
                if (idUsuario != 0)
                {
                    database.SetProcedure("SP_GetTicketsCountByUser");
                    database.SetParameter("@idUsuario", idUsuario);
                }
                else
                    database.SetQuery("SELECT * FROM VW_GetTicketsCount;");
                database.ExecQuery();

                while (database.reader.Read())
                {
                    DashboardDTO ticketCount = new DashboardDTO
                    {
                        Estado = database.reader["Estado"].ToString(),
                        Cantidad = Convert.ToInt32(database.reader["Cantidad"])
                    };
                    ticketsCount.Add(ticketCount);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return ticketsCount;
        }
    }
}
