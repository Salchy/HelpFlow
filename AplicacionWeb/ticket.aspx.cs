using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using AccesoDatos;
using DTO;

namespace AplicacionWeb
{
    public partial class ticket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TicketDatos ticketDatos = new TicketDatos();

            if (!IsPostBack)
            {
                string idRuta = Page.RouteData.Values["id"] as string;

                if (string.IsNullOrEmpty(idRuta))
                {
                    //CargarDatosTicket(idRuta);
                    // if (id ticket no existe)
                    // El ticket no existe
                    Response.Redirect("main.aspx");
                }
                try
                {
                    int ticketID = int.Parse(idRuta);
                    Ticket ticket = ticketDatos.ObtenerTicket(ticketID);

                    tituloTicket.InnerText = "Ticket #" + ticketID;

                    lblTitulo.Text = ticket.Asunto;
                    lblSolicitante.Text = ticket.UsuarioCreador.Nombre;
                    lblDescripcion.Text = ticket.Descripcion;
                    lblEstado.Text = ticket.Estado.NombreEstado;
                    //lblUsuarioAsignado.Text = ticket.UsuarioAsignado.Nombre;
                    lblFecha.Text = ticket.FechaCreacion.ToString("dd/MM/yyyy");
                    MostrarEstado(ticket);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void MostrarEstado(Ticket ticket)
        {
            int IdEstado = ticket.Estado.Id;

            switch (IdEstado)
            {
                case 0:
                    lblEstado.CssClass = "badge bg-primary";
                    break;

                case 1:
                    lblEstado.CssClass = "badge bg-warning text-dark";
                    break;

                case 2:
                    lblEstado.CssClass = "badge bg-success";
                    break;

                case 3:
                    lblEstado.CssClass = "badge bg-secondary";
                    break;

                default:
                    lblEstado.CssClass = "badge bg-dark";
                    break;
            }
        }

        private void mostrarCommits(int ticketID)
        {
            TicketDatos ticketDatos = new TicketDatos();

            try
            {

            }
            catch (Exception Ex)
            {

                throw;
            }
        }
    }
}