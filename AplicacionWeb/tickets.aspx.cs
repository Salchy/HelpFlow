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
        private List<Categoria> Categorias
        {
            get
            {
                if (Session["Categorias"] == null)
                    Session["Categorias"] = new List<Categoria>();
                return (List<Categoria>)Session["Categorias"];
            }
            set
            {
                Session["Categorias"] = value;
            }
        }

        private List<SubCategoria> SubCategorias
        {
            get
            {
                if (Session["SubCategorias"] == null)
                    Session["SubCategorias"] = new List<SubCategoria>();
                return (List<SubCategoria>)Session["SubCategorias"];
            }
            set
            {
                Session["SubCategorias"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            TicketDatos ticketDatos = new TicketDatos();

            if (!IsPostBack)
            {
                try
                {
                    List<TicketDTO> listaTickets = ticketDatos.ObtenerListaTickets();
                    printTicketsInGridlist(listaTickets);

                    DatosClasificacionTicket datosClasificacion = new DatosClasificacionTicket();

                    Categorias = datosClasificacion.listarCategorias();
                    SubCategorias = datosClasificacion.listarSubCategorias();

                    bindDropDownList(ddlCategoria, Categorias);
                    bindDropDownList(ddlSubCategoria, SubCategorias);

                    ddlCategoria.Items.Insert(0, new ListItem("Todas las categorías", "-1"));
                    ddlSubCategoria.Items.Insert(0, new ListItem("Todas las subcategorías", "-1"));
                }
                catch (Exception ex)
                {
                    // Manejo de errores, por ejemplo, mostrar un mensaje al usuario
                }
            }
        }

        private void printTicketsInGridlist(List<TicketDTO> listaTickets)
        {
            dataGridTickets.DataSource = null;
            dataGridTickets.DataSource = listaTickets;
            dataGridTickets.DataBind();
        }

        private void bindDropDownList(DropDownList dropDown, List<Categoria> lista)
        {
            dropDown.DataSource = null;
            dropDown.DataSource = lista;
            dropDown.DataTextField = "Nombre";
            dropDown.DataValueField = "Id";
            dropDown.DataBind();
        }

        private void bindDropDownList(DropDownList dropDown, List<SubCategoria> lista)
        {
            dropDown.DataSource = null;
            dropDown.DataSource = lista;
            dropDown.DataTextField = "Nombre";
            dropDown.DataValueField = "Id";
            dropDown.DataBind();
        }

        protected string GetEstadoCss(string estado)
        {
            switch (estado.ToLower())
            {
                case "solicitado":
                    return "badge bg-primary";
                case "en progreso":
                    return "badge bg-warning text-dark";
                case "resuelto":
                    return "badge bg-success";
                case "cerrado":
                    return "badge bg-secondary";
                default:
                    return "badge bg-light text-dark";
            }
        }

        private List<TicketDTO> applyFilters()
        {
            TicketDatos ticketDatos = new TicketDatos();

            return ticketDatos.ObtenerListaTicketsFiltro(ddlCategoria.SelectedValue, ddlSubCategoria.SelectedValue, ddlEstado.SelectedItem.Text, ddlCriterio.SelectedItem.Text, txtId.Text.Trim());
        }
        
        protected void ddlEstadoFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            printTicketsInGridlist(applyFilters());
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCategoria = int.Parse(ddlCategoria.SelectedValue);
            ddlSubCategoria.Items.Clear();
            ddlSubCategoria.DataSource = null;

            List<SubCategoria> filtro = idCategoria.ToString() == "-1" ? SubCategorias : SubCategorias.Where(sc => sc.IdCategoria == idCategoria).ToList();

            bindDropDownList(ddlSubCategoria, filtro);

            ddlSubCategoria.Items.Insert(0, new ListItem("Todas las subcategorías", "-1"));

            printTicketsInGridlist(applyFilters());
        }

        protected void ddlSubCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            printTicketsInGridlist(applyFilters());
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            printTicketsInGridlist(applyFilters());
        }
    }
}
