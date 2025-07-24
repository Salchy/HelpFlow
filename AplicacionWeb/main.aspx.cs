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
    public partial class main1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();

            if (!IsPostBack)
            {
                try
                {
                    List<DashboardDTO> listaTickets = dashboard.GetTicketsCount();
                    for (int i = 0; i < listaTickets.Count; i++)
                    {
                        string cantidad = listaTickets[i].Cantidad.ToString();

                        switch (listaTickets[i].Estado)
                        {
                            case "Solicitado":
                                lblAbiertos.Text = cantidad;
                                break;
                            case "En progreso":
                                lblEnProgreso.Text = cantidad;
                                break;
                            case "Resuelto":
                                lblResueltos.Text = cantidad;
                                break;
                            case "Cerrado":
                                lblCerrados.Text = cantidad;
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Error al cargar los datos: " + ex.Message);
                }
            }
        }
    }
}