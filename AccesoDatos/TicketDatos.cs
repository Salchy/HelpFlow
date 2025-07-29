using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
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
                database.SetQuery("SELECT TicketID, Titulo, Nombre, FechaCreacion, FechaActualizacion, NombreEstado, Colaboradores FROM VW_GetAllTicketsWithColaborators");
                database.ExecQuery();

                while (database.reader.Read())
                {
                    TicketDTO ticket = new TicketDTO
                    {
                        Id = Convert.ToInt32(database.reader["TicketID"]),
                        Asunto = database.reader["Titulo"].ToString(),
                        UsuarioCreador = database.reader["Nombre"].ToString(),
                        Estado = database.reader["NombreEstado"].ToString(),
                        FechaCreacion = database.reader.GetDateTime(database.reader.GetOrdinal("FechaCreacion")),
                        Colaboradores = database.reader["Colaboradores"].ToString()
                    };
                    listaTickets.Add(ticket);
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
            return listaTickets;
        }

        public List<UsuarioColaboradorDTO> ObtenerColaboradores(int IdTicket)
        {
            try
            {
                List<UsuarioColaboradorDTO> colaboradores = new List<UsuarioColaboradorDTO>();

                database.SetQuery("SELECT * FROM VW_GetCollaboratorsPerTicket WHERE IdTicket = @IdTicket");
                database.SetParameter("@IdTicket", IdTicket);
                database.ExecQuery();

                while (database.reader.Read())
                {
                    int idUsuario = Convert.ToInt32(database.reader["IdUsuario"]);
                    string nombre = database.reader["Nombre"].ToString();
                    string correo = database.reader["Correo"].ToString();
                    UsuarioColaboradorDTO colaborador = new UsuarioColaboradorDTO(idUsuario, nombre, correo);
                    colaboradores.Add(colaborador);
                }
                return colaboradores;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<TicketDTO> ObtenerListaTickets(int idUsuario) // Obtener listado de tickets de un usuario
        {
            List<TicketDTO> listaTickets = new List<TicketDTO>();
            try
            {
                database.SetQuery("SELECT TicketID, Titulo, Nombre, FechaCreacion, FechaActualizacion, NombreEstado FROM VW_GetAllTickets WHERE @IdUsuarioCreador = IdUsuario;");
                database.SetParameter("@IdUsuarioCreador", idUsuario);
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
            }
            finally
            {
                database.CloseConnection();
            }
            return listaTickets;
        }

        public Ticket ObtenerTicket(int id)
        {
            try
            {
                database.SetQuery("SELECT * FROM VW_GetTicketInfo WHERE Id = @idTicket");
                database.SetParameter("@idTicket", id);
                database.ExecQuery();
                
                Ticket Ticket = new Ticket();

                if (database.reader.Read())
                {
                    // Datos del estado
                    int idEstado = Convert.ToInt32(database.reader["IdEstado"]);
                    string nombreEstado = database.reader["NombreEstado"].ToString();
                    Estado estado = new Estado { Id = idEstado, NombreEstado = nombreEstado };

                    // Datos del usuario creador
                    int idUsuarioCreador = Convert.ToInt32(database.reader["IdUsuarioCreador"]);
                    string nombreCreador = database.reader["Nombre"].ToString();
                    //string tipoUsuario = Convert.ToBoolean(database.reader["TipoUsuario"]) ? "Usuario" : "Administrador";
                    int tipoUsuario = Convert.ToInt32(database.reader["TipoUsuario"]);
                    string correo = database.reader["Correo"].ToString();

                    Usuario usuarioCreador = new Usuario(idUsuarioCreador, nombreCreador, correo, tipoUsuario);

                    // Datos del ticket
                    int ticketId = Convert.ToInt32(database.reader["Id"]);
                    string asunto = database.reader["Titulo"].ToString();
                    DateTime fechaCreacion = database.reader.GetDateTime(database.reader.GetOrdinal("FechaCreacion"));
                    DateTime fechaUltimaActualizacion = database.reader.GetDateTime(database.reader.GetOrdinal("FechaActualizacion"));
                    string descripcion = database.reader["Descripcion"].ToString();

                    return new Ticket(ticketId, usuarioCreador, asunto, estado, fechaCreacion, fechaUltimaActualizacion, descripcion);
                }
                return Ticket;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //public void CrearTicket(Ticket ticket)
        //{
        //    try
        //    {
        //        database.SetProcedure("SP_CrearTicket");
        //        database.SetParameter("@IdUsuarioCreador", ticket.UsuarioCreador.Id);
        //        database.SetParameter("@Titulo", ticket.Asunto);
        //        database.SetParameter("@Descripcion", ticket.Descripcion);
        //        database.ExecNonQuery();
        //    }
        //    catch (Exception Ex)
        //    {
        //        throw Ex;
        //    }
        //    finally
        //    {
        //        database.CloseConnection();
        //    }
        //}
    }
}