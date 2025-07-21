using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using DTO;

namespace AccesoDatos
{
    public class TicketDatos
    {
        // Acá tendrían que ir los métodos, como obtenerTicket (id), crearTicket(ticket), actualizarTicket(ticket), eliminarTicket(id), ObtenerTodos, etc.
        private Database database;
        public TicketDatos()
        {
            database = new Database();
        }

        //public Ticket setTicketData(SqlDataReader data)
        //{
        //    UsuarioDatos usuarioDatos = new UsuarioDatos();

        //    Ticket ticket = new Ticket(
        //        Convert.ToInt32(data["TicketID"]),
        //        ,
        //        data["Asunto"].ToString(),
        //        data["Estado"].ToString(),
        //        Convert.ToDateTime(data["FechaCreacion"]),
        //        data.IsDBNull(5) ? null : data["Descripcion"].ToString()
        //    );
        //    return ticket;
        //}

        public List<TicketDTO> ObtenerListaTickets()
        {
            List<TicketDTO> listaTickets = new List<TicketDTO>();
            try
            {
                database.SetQuery("SELECT TicketID, Titulo, Nombre, FechaCreacion, FechaActualizacion, NombreEstado FROM VW_GetAllTickets");
                database.ExecQuery();

                while (database.reader.Read())
                {
                    TicketDTO ticket = new TicketDTO
                    {
                        Id = Convert.ToInt32(database.reader["TicketID"]),
                        Asunto = database.reader["Titulo"].ToString(),
                        UsuarioCreador = database.reader["Nombre"].ToString(),
                        Estado = database.reader["NombreEstado"].ToString(),
                        FechaCreacion = database.reader.GetDateTime(database.reader.GetOrdinal("FechaCreacion"))
                        //Colaboradores = !database.reader.IsDBNull(6) ? database.reader.GetString(6).Split(',').ToList() : new List<string>()
                    };
                    listaTickets.Add(ticket);
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            } finally
            {
                database.CloseConnection();
            }
            return listaTickets;
        }

        //public Ticket ObtenerTicket(int id)
        //{
        //    try
        //    {
        //        database.SetQuery("SELECT * FROM VW_GetAllTickets WHERE TicketID = @id");
        //        database.SetParameter("@id", id);
        //        database.ExecQuery();
        //        if (database.reader.Read())
        //        {
        //            int ticketId = database.reader.GetInt32(0);
        //            string asunto = database.reader.GetString(1);
        //            string usuarioCreador = database.reader.GetString(2);
        //            string estado = database.reader.GetString(3);
        //            DateTime fechaCreacion = database.reader.GetDateTime(4);
        //            string descripcion = database.reader.IsDBNull(5) ? null : database.reader.GetString(5);
        //            List<string> colaboradores = new List<string>();
        //            if (!database.reader.IsDBNull(6))
        //            {
        //                colaboradores = database.reader.GetString(6).Split(',').ToList();
        //            }
        //            return new Ticket(ticketId, usuarioCreador, asunto, estado, fechaCreacion, descripcion, colaboradores);
        //        }
        //    }
        //    catch (Exception Ex)
        //    {

        //        throw Ex;
        //    }
        //}
    }
}