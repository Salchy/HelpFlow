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
    public partial class misTickets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TicketDatos ticketDatos = new TicketDatos();

            if (!IsPostBack)
            {
                try
                {
                    Usuario usuario = (Usuario)Session["Usuario"];
                    List<TicketDTO> listaTickets = ticketDatos.ObtenerListaTickets(usuario.Id);
                    bindearDatos(listaTickets);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void bindearDatos(List<TicketDTO> tickets)
        {
            gvTickets.DataSource = null;
            gvTickets.DataSource = tickets;
            gvTickets.DataBind();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        public string ObtenerBadgeEstado(string estado)
        {
            string clase = "bg-light text-dark"; // valor por defecto

            switch (estado.ToLower())
            {
                case "solicitado":
                    clase = "bg-primary";
                    break;
                case "en progreso":
                    clase = "bg-warning text-dark";
                    break;
                case "resuelto":
                    clase = "bg-success";
                    break;
                case "cerrado":
                    clase = "bg-secondary";
                    break;
            }

            return $"<span class='badge {clase}'>{estado}</span>";
        }

    }
}