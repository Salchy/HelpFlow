using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        public bool InsertCommit(CommitDTO commit)
        {
            try
            {
                database.SetProcedure("SP_RegistrarCommit");

                database.SetParameter("@typeCommit", commit.TipoCommit);
                database.SetParameter("@idTicket", commit.IdTicketRelacionado);
                database.SetParameter("@idAutor", commit.IdAutor);
                database.SetParameter("@message", commit.Mensaje);

                int newID = database.ExecScalar();
                commit.Id = newID;
                commit.Fecha = DateTime.Now;
                return true;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<CommitDTO> GetTicketCommitsDTOs(int idTicket, bool isClient = false)
        {
            List<CommitDTO> commits = new List<CommitDTO>();
            try
            {
                string query = "SELECT * FROM VW_GetCommits WHERE IdTicketRelacionado = @IdTicket";
                if (isClient)
                    query += " AND TipoCommit = 0";
                database.SetQuery(query);
                database.SetParameter("@IdTicket", idTicket);
                database.ExecQuery();

                while (database.reader.Read())
                {
                    CommitDTO commit = new CommitDTO
                    {
                        Id = Convert.ToInt32(database.reader["Id"]),
                        IdAutor = Convert.ToInt32(database.reader["IdAutor"]),
                        AutorNombre = database.reader["Nombre"].ToString(),
                        Fecha = database.reader.GetDateTime(database.reader.GetOrdinal("Fecha")),
                        Mensaje = database.reader["Mensaje"].ToString(),
                        IdTicketRelacionado = Convert.ToInt32(database.reader["IdTicketRelacionado"]),
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