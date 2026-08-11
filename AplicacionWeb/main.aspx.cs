using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccesoDatos;
using DTO;
using Dominio;

namespace AplicacionWeb
{
    public partial class main1 : System.Web.UI.Page
    {
        // Guardo el filtro seleccionado
        private string FiltroActual
        {
            get { return ViewState["FiltroActual"] as string ?? "Todo"; }
            set { ViewState["FiltroActual"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();

            if (!IsPostBack)
            {
                CargarDashboard(FiltroActual);

                //    try
                //    {
                //        List<DashboardDTO> listaTickets;
                //        Usuario usuario = UsuarioDatos.UsuarioActual(Session["Usuario"]);
                //        if (usuario == null)
                //        {
                //            Response.Redirect("Login.aspx", false);
                //            return;
                //        }

                //        if ((int)usuario.TipoUsuario == 1) // Es usuario, filtrar los tickets
                //            listaTickets = dashboard.GetTicketsCount(usuario.Id);
                //        else
                //            listaTickets = dashboard.GetTicketsCount();
                //        for (int i = 0; i < listaTickets.Count; i++)
                //        {
                //            string cantidad = listaTickets[i].Cantidad.ToString();

                //            switch (listaTickets[i].Estado)
                //            {
                //                case "Solicitado":
                //                    lblSolicitados.Text = cantidad;
                //                    break;
                //                case "En progreso":
                //                    lblEnProgreso.Text = cantidad;
                //                    break;
                //                case "Resuelto":
                //                    lblResueltos.Text = cantidad;
                //                    break;
                //                case "Cerrado":
                //                    lblCerrados.Text = cantidad;
                //                    break;
                //            }
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        Response.Write("Error al cargar los datos: " + ex.Message);
                //    }
                //}
            }
        }

        protected void btnFiltro_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            FiltroActual = btn.CommandArgument;
            CargarDashboard(FiltroActual);
        }

        private void CargarDashboard(string filtro)
        {
            try
            {
                Usuario usuario = UsuarioDatos.UsuarioActual(Session["Usuario"]);
                if (usuario == null)
                {
                    Response.Redirect("Login.aspx", false);
                    return;
                }

                Dashboard dashboard = new Dashboard();
                List<DashboardDTO> listaTickets = ((int)usuario.TipoUsuario == 1) ? dashboard.GetTicketsCount(usuario.Id) : dashboard.GetTicketsCount();

                listaTickets = FiltrarPorFecha(listaTickets, filtro);

                // Sumamos las cantidades por estado (hay una fila por Estado + Fecha)
                var totalesPorEstado = listaTickets.GroupBy(t => t.Estado).ToDictionary(g => g.Key, g => g.Sum(t => t.Cantidad));

                lblSolicitados.Text = totalesPorEstado.ContainsKey("Solicitado") ? totalesPorEstado["Solicitado"].ToString() : "0";
                lblEnProgreso.Text = totalesPorEstado.ContainsKey("En progreso") ? totalesPorEstado["En progreso"].ToString() : "0";
                lblResueltos.Text = totalesPorEstado.ContainsKey("Resuelto") ? totalesPorEstado["Resuelto"].ToString() : "0";
                lblCerrados.Text = totalesPorEstado.ContainsKey("Cerrado") ? totalesPorEstado["Cerrado"].ToString() : "0";

                ActualizarBotonActivo(filtro);
            }
            catch (Exception ex)
            {
                Response.Write("Error al cargar los datos: " + ex.Message);
            }
        }
        private List<DashboardDTO> FiltrarPorFecha(List<DashboardDTO> lista, string filtro)
        {
            DateTime hoy = DateTime.Today;

            switch (filtro)
            {
                case "Hoy":
                    return lista.Where(t => t.Fecha.Date == hoy).ToList();

                case "Semana":
                    // Lunes de esta semana hasta hoy
                    int diasDesdeElLunes = ((int)hoy.DayOfWeek == 0) ? 6 : (int)hoy.DayOfWeek - 1;
                    DateTime inicioSemana = hoy.AddDays(-diasDesdeElLunes);
                    return lista.Where(t => t.Fecha.Date >= inicioSemana && t.Fecha.Date <= hoy).ToList();

                case "Mes":
                    DateTime inicioMes = new DateTime(hoy.Year, hoy.Month, 1);
                    return lista.Where(t => t.Fecha.Date >= inicioMes && t.Fecha.Date <= hoy).ToList();

                case "Todo":
                default:
                    return lista;
            }
        }

        private void ActualizarBotonActivo(string filtro)
        {
            const string inactivo = "btn btn-outline-light mx-1";
            const string activo = "btn btn-light mx-1 fw-bold";

            btnHoy.CssClass = inactivo;
            btnSemana.CssClass = inactivo;
            btnMes.CssClass = inactivo;
            btnTodo.CssClass = inactivo;

            switch (filtro)
            {
                case "Hoy": btnHoy.CssClass = activo; break;
                case "Semana": btnSemana.CssClass = activo; break;
                case "Mes": btnMes.CssClass = activo; break;
                case "Todo": btnTodo.CssClass = activo; break;
            }
        }
    }
}