using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccesoDatos;
using AplicacionWeb.Helpers;
using Dominio;
using DTO;

namespace AplicacionWeb
{
    public partial class crearTicket : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                try
                {
                    DatosClasificacionTicket datosClasificacion = new DatosClasificacionTicket();

                    Categorias = datosClasificacion.listarCategorias();
                    SubCategorias = datosClasificacion.listarSubCategorias();

                    ddlCategoria.DataSource = Categorias;
                    ddlCategoria.DataTextField = "Nombre";
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataBind();
                    ddlCategoria.Items.Insert(0, new ListItem("-- Seleccione una categoría --", "0"));

                    ddlSubCategoria.Items.Insert(0, new ListItem("-- Seleccione una categoría --", "0"));
                }
                catch (Exception Ex)
                {
                    Modal.Mostrar(this, "Error", "No se pudieron cargar las categorías y subcategorías. Por favor, inténtelo más tarde.", "error");
                }
            }
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCategoria = int.Parse(ddlCategoria.SelectedValue);
            ddlSubCategoria.Items.Clear();
            ddlSubCategoria.DataSource = null;
            if (idCategoria == 0)
            {
                ddlSubCategoria.Items.Insert(0, new ListItem("-- Seleccione una categoría --", "0"));
                return;
            }
            try
            {
                List<SubCategoria> subCategoriasFiltradas = SubCategorias.Where(sc => sc.IdCategoria == idCategoria).ToList();

                ddlSubCategoria.DataSource = subCategoriasFiltradas;
                ddlSubCategoria.DataTextField = "Nombre";
                ddlSubCategoria.DataValueField = "Id";
                ddlSubCategoria.DataBind();
                ddlSubCategoria.Items.Insert(0, new ListItem("-- Seleccione una subcategoría --", "0"));
            }
            catch (Exception Ex)
            {
                Modal.Mostrar(this, "Error", "No se pudieron cargar las subcategorías. Por favor, inténtelo más tarde.", "error");
            }
        }

        protected void btnCrearTicket_Click(object sender, EventArgs e)
        {
            Usuario user = UsuarioDatos.UsuarioActual(Session["Usuario"]);
            if (user == null)
            {
                Modal.Mostrar(this, "Error", "Debe iniciar sesión para crear un ticket.", "error");
                return;
            }
            string descripcion = txtDescripcion.Text.Trim();
            if (descripcion.Length < 5)
            {
                Modal.Mostrar(this, "Error", "La descripción del ticket debe tener al menos 5 caracteres.", "error");
                return;
            }
            if (descripcion.Length > 2000)
            {
                Modal.Mostrar(this, "Error", "La descripción del ticket no puede exceder los 2000 caracteres.", "error");
                return;
            }
            TicketDatos datosTicket = new TicketDatos();
            try
            {
                if (ddlCategoria.SelectedValue == "0" || ddlSubCategoria.SelectedValue == "0")
                {
                    Modal.Mostrar(this, "Error", "Debe seleccionar una categoría y una subcategoría.", "error");
                    return;
                }
                TicketCreacionDTO nuevoTicket = new TicketCreacionDTO
                {
                    IdCreador = user.Id,
                    IdSubCategoria = int.Parse(ddlSubCategoria.SelectedValue),
                    Descripcion = txtDescripcion.Text,
                };
                datosTicket.CrearTicket(nuevoTicket);
                if (nuevoTicket.IdCreador == -1)
                {
                    Modal.Mostrar(this, "Error", "No se pudo crear el ticket. Por favor, inténtelo más tarde.", "error");
                    return;
                }
                Modal.Mostrar(this, "Éxito", "El ticket se ha creado correctamente.", "exito");
                txtDescripcion.Text = "";
            }
            catch (Exception Ex)
            {
                Modal.Mostrar(this, "Error", "No se pudo crear el ticket. Por favor, inténtelo más tarde.", "error");
            }
        }
    }
}