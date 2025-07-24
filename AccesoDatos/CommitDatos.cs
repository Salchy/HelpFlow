using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace AccesoDatos
{
    public class CommitDatos
    {
        private Database database;
        public CommitDatos()
        {
            database = new Database();
        }

        public List<CommitDTO> GetTicketCommitsDTOs(int idTicket)
        {
            List<CommitDTO> commits = new List<CommitDTO>();
            try
            {
                database.SetQuery("SELECT * FROM VW_GetCommits WHERE IdTicketRelacionado = @IdTicket");
                database.SetParameter("@IdTicket", idTicket);
                database.ExecQuery();

                while (database.reader.Read())
                {
                    CommitDTO commit = new CommitDTO
                    {
                        Id = Convert.ToInt32(database.reader["Id"]),
                        Autor = database.reader["Nombre"].ToString(),
                        Fecha = database.reader.GetDateTime(database.reader.GetOrdinal("Fecha")),
                        Mensaje = database.reader["Mensaje"].ToString(),
                        TipoCommit = Convert.ToBoolean(database.reader["TipoCommit"])
                    };
                    commits.Add(commit);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return commits;
        }
    }
}