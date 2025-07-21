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
    public partial class tickets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TicketDatos ticketDatos = new TicketDatos();

            if (!IsPostBack)
            {
                try
                {
                    List<TicketDTO> listaTickets = ticketDatos.ObtenerListaTickets();
                    dataGridTickets.DataSource = listaTickets;
                    dataGridTickets.DataBind();
                }
                catch (Exception ex)
                {
                    // Manejo de errores, por ejemplo, mostrar un mensaje al usuario
                }
            }
        }
    }
}