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

        private TicketCreacionDTO ticket
        {
            get
            {
                if (Session["TicketEdicion"] == null)
                    Session["TicketEdicion"] = new TicketCreacionDTO();
                return (TicketCreacionDTO)Session["TicketEdicion"];
            }
            set
            {
                Session["TicketEdicion"] = value;
            }
        }

        // Para saber lo que se está editando, para saber qué valores actualizar de la db
        private string editingField;

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
                    panelEdicion.Visible = false;

                    string queryString = Request.QueryString["id"];

                    // Ver si recibió por parametro URL un ticket ID, para ver si es modificacion de un ticket, o si es una creacion de un ticket nuevo
                    if (queryString != null)
                    {
                        int ticketID = int.Parse(queryString);
                        TicketDatos ticketDatos = new TicketDatos();
                        ticket = ticketDatos.GetTicket(ticketID);
                        
                        if (ticket.Id == -1)
                        {
                            Modal.Mostrar(this, "Error", "El ticket no existe o no está disponible.", "error");
                            Response.Redirect("tickets.aspx", false);
                            return;
                        }
                        lblTitulo.InnerText = "Modificar Ticket";
                        btnCrearTicket.Text = "Guardar";

                        hfTicketID.Value = ticket.Id.ToString();
                        string action = Request.QueryString["action"];

                        if (action == null)
                        {
                            modifyTittle();
                            modifySolicitante();
                            modifyDescription();
                            modifySupporters();
                            return;
                        }
                        tittleSection.Visible = false;
                        descriptionSection.Visible = false;
                        statusSection.Visible = false;
                        ownerSection.Visible = false;
                        supportersSection.Visible = false;

                        switch (action)
                        {
                            case "modificarTitulo":
                                modifyTittle();
                                break;
                            case "modificarSoliciante":
                                modifySolicitante();
                                break;
                            case "modificarFechaCreacion":
                                
                                break;
                            case "modificarDescripcion":
                                modifyDescription();
                                break;
                            case "modificarAsignados":
                                modifySupporters();
                                break;
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Modal.Mostrar(this, "Error", "No se pudieron cargar las categorías y subcategorías. Por favor, inténtelo más tarde.", "error");
                }
            }
        }

        private void modifyTittle()
        {
            // Documentar, porque nose bien lo que hice, pero funciona JAJAJA
            string categoriaTicket = SubCategorias.FirstOrDefault(sc => sc.Id == ticket.IdSubCategoria)?.IdCategoria.ToString() ?? "0";

            ddlSubCategoria.DataSource = SubCategorias.Where(sc => sc.IdCategoria == int.Parse(categoriaTicket)).ToList();
            ddlSubCategoria.DataTextField = "Nombre";
            ddlSubCategoria.DataValueField = "Id";
            ddlSubCategoria.DataBind();

            // El id subcategoria del ticket, va a hacer referencia al id subcategoria de la lista de subcategorias
            // y el idCategoriaPadre de la subcategoria esa, va a hacer referencia al idCategoria de la lista de categorias
            ddlCategoria.SelectedValue = categoriaTicket;

            ddlSubCategoria.SelectedValue = ticket.IdSubCategoria.ToString();

            tittleSection.Visible = true;
        }

        private void modifySolicitante()
        {
            ddlOwner.DataSource = new UsuarioDatos().GetUsuarios();
            ddlOwner.DataTextField = "Nombre";
            ddlOwner.DataValueField = "Id";
            ddlOwner.DataBind();
            ddlOwner.SelectedValue = ticket.IdCreador.ToString();

            panelEdicion.Visible = true;
            ownerSection.Visible = true;
        }

        private void modifyDescription()
        {
            txtDescripcion.Text = ticket.Descripcion;

            descriptionSection.Visible = true;
        }

        private void modifySupporters()
        {
            lstDisponibles.DataSource = new UsuarioDatos().GetSupporters();
            lstDisponibles.DataTextField = "Nombre";
            lstDisponibles.DataValueField = "Id";
            lstDisponibles.DataBind();

            panelEdicion.Visible = true;
            supportersSection.Visible = true;
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
                Modal.Mostrar(this, "Éxito", "El ticket se ha creado correctamente.\nSu número de ticket es el TKT-" + nuevoTicket.Id + ".", "exito");
                Response.Redirect("misTickets.aspx", true);
            }
            catch (Exception Ex)
            {
                Modal.Mostrar(this, "Error", "No se pudo crear el ticket. Por favor, inténtelo más tarde.", "error");
            }
        }
    }
}