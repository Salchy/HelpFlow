using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
                    List<TicketDTO> listaTickets = ticketDatos.ObtenerListaTickets();
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
                case "abierto":
                    clase = "bg-secondary";
                    break;
                case "en progreso":
                    clase = "bg-warning text-dark";
                    break;
                case "cerrado":
                    clase = "bg-success";
                    break;
                case "cancelado":
                    clase = "bg-danger";
                    break;
            }

            return $"<span class='badge {clase}'>{estado}</span>";
        }

    }
}