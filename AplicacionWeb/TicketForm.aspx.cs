using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        private string editingField
        {
            get
            {
                if (Session["editingField"] == null)
                    Session["editingField"] = null;
                return Session["editingField"] as string;
            }
            set
            {
                Session["editingField"] = value;
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
                    panelEdicion.Visible = false;
                    hfTicketID.Value = "0";

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

                        cargarColaboradores(ticketID);

                        editingField = action != null ? action : "all";

                        if (action == null)
                        {
                            modifyTittle();
                            modifySolicitante();
                            modifyDescription();
                            modifySupporters();
                            modifyDate();
                            return;
                        }
                        tittleSection.Visible = false;
                        descriptionSection.Visible = false;
                        statusSection.Visible = false;
                        ownerSection.Visible = false;
                        supportersSection.Visible = false;
                        dateSection.Visible = false;

                        switch (action)
                        {
                            case "modificarTitulo":
                                modifyTittle();
                                break;
                            case "modificarSoliciante":
                                modifySolicitante();
                                break;
                            case "modificarFechaCreacion":
                                modifyDate();
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

        private void cargarColaboradores(int ticketID)
        {
            List<UsuarioColaboradorDTO> colaboradores = new TicketDatos().ObtenerColaboradores(ticketID);
            lstAsignados.DataSource = colaboradores;
            lstAsignados.DataTextField = "Nombre";
            lstAsignados.DataValueField = "Id";
            lstAsignados.DataBind();

            ticket.IdColaboradores = colaboradores.Select(c => c.Id).ToList();
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

            // Removerlos de la lista de supporters disponibles, si es que ya estan asignados:
            for (int i = 0; i < lstAsignados.Items.Count; i++)
                lstDisponibles.Items.Remove(lstDisponibles.Items.FindByValue(lstAsignados.Items[i].Value));

            panelEdicion.Visible = true;
            supportersSection.Visible = true;
        }

        private void modifyDate()
        {
            DateTime date = DateTime.Now;

            txtFecha.Value = date.ToString("yyyy-MM-dd");
            txtHora.Value = date.ToString("HH:mm");

            panelEdicion.Visible = true;
            dateSection.Visible = true;
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
            int id = Convert.ToInt32(hfTicketID.Value);
            if (id == 0) // Es un ticket nuevo
            {
                crearNuevoTicket();
            }
            else
            { // Es una modificacion de un ticket existente
                modificarTicket(id);
            }
        }

        private void crearNuevoTicket()
        {
            Usuario user = UsuarioDatos.UsuarioActual(Session["Usuario"]);
            TicketDatos datosTicket = new TicketDatos();
            try
            {
                if (!validarDescripcion())
                    return;
                if (!validarCategorización())
                    return;

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

                MailHelper.SendEmail(user.Correo, "Novedades Ticket - #" + nuevoTicket.Id, "Se creó un nuevo ticket. Su número de ticket es el TKT-" + nuevoTicket.Id, user.Nombre, nuevoTicket.Id.ToString());
                notificarSupporters(nuevoTicket);

                Response.Redirect("misTickets.aspx", true);
            }
            catch (Exception Ex)
            {
                Modal.Mostrar(this, "Error", "No se pudo crear el ticket. Por favor, inténtelo más tarde.", "error");
            }
        }

        private void notificarSupporters(TicketCreacionDTO ticket)
        {
            UsuarioDatos usuarioDatos = new UsuarioDatos();

            try
            {
                foreach (UsuarioDTO supporter in usuarioDatos.GetSupporters())
                {
                    MailHelper.SendEmail(supporter.Correo, "Nuevo Ticket - #" + ticket.Id, "Se creó un nuevo ticket. Con el asunto " + new DatosClasificacionTicket().getAsunto(ticket.IdEstado), supporter.Nombre, ticket.Id.ToString());
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private bool validarDescripcion()
        {
            string descripcion = txtDescripcion.Text.Trim();
            if (descripcion.Length < 5)
            {
                Modal.Mostrar(this, "Error", "La descripción del ticket debe tener al menos 5 caracteres.", "error");
                return false;
            }
            if (descripcion.Length > 2000)
            {
                Modal.Mostrar(this, "Error", "La descripción del ticket no puede exceder los 2000 caracteres.", "error");
                return false;
            }
            return true;
        }

        private bool validarCategorización()
        {
            if (ddlCategoria.SelectedValue == "0" || ddlSubCategoria.SelectedValue == "0")
            {
                Modal.Mostrar(this, "Error", "Debe seleccionar una categoría y una subcategoría.", "error");
                return false;
            }
            return true;
        }

        private bool modificarTicket(int id)
        {
            TicketDatos datosTicket = new TicketDatos();
            CommitDatos datosCommit = new CommitDatos();
            try
            {
                if (editingField == "all")
                {
                    if (!validarCategorización())
                        return false;
                    if (!validarDescripcion())
                        return false;
                    ticket.IdSubCategoria = int.Parse(ddlSubCategoria.SelectedValue);
                    ticket.IdCreador = int.Parse(ddlOwner.SelectedValue);
                    ticket.Descripcion = txtDescripcion.Text;
                    modificarSupporters();
                }
                else
                {
                    switch (editingField)
                    {
                        case "modificarTitulo":
                            if (!validarCategorización())
                                return false;
                            int newIdSubCategoria = int.Parse(ddlSubCategoria.SelectedValue);
                            DatosClasificacionTicket adminCatsAndSubCats = new DatosClasificacionTicket();

                            datosCommit.registrarLog(((Usuario)UsuarioDatos.UsuarioActual(Session["Usuario"])).Id, ticket.Id, "modificó el asunto del ticket de '" + adminCatsAndSubCats.getAsunto(ticket.IdSubCategoria) + "' a '" + adminCatsAndSubCats.getAsunto(newIdSubCategoria) + "'.");
                            ticket.IdSubCategoria = newIdSubCategoria;
                            break;
                        case "modificarSoliciante":
                            int newIdCreador = int.Parse(ddlOwner.SelectedValue);
                            UsuarioDatos adminUsers = new UsuarioDatos();
                            datosCommit.registrarLog(((Usuario)UsuarioDatos.UsuarioActual(Session["Usuario"])).Id, ticket.Id, "modificó el solicitante del ticket de '" + adminUsers.GetUsuarioDTO(ticket.IdCreador).Nombre + "' a '" + adminUsers.GetUsuarioDTO(newIdCreador).Nombre + "'.");
                            ticket.IdCreador = newIdCreador;
                            break;
                        case "modificarDescripcion":
                            if (!validarDescripcion())
                                return false;
                            datosCommit.registrarLog(((Usuario)UsuarioDatos.UsuarioActual(Session["Usuario"])).Id, ticket.Id, "modificó la descripción del ticket.");
                            ticket.Descripcion = txtDescripcion.Text;
                            break;
                        case "modificarFechaCreacion":
                            string fechaSeleccionada = txtFecha.Value;
                            string horaSeleccionada = txtHora.Value;

                            if (string.IsNullOrEmpty(fechaSeleccionada) || string.IsNullOrEmpty(horaSeleccionada))
                                return false;

                            string fechaHoraIso = $"{fechaSeleccionada}T{horaSeleccionada}";

                            if (!DateTime.TryParse(fechaHoraIso, out DateTime fechaHora))
                                return false;
                            datosCommit.registrarLog(((Usuario)UsuarioDatos.UsuarioActual(Session["Usuario"])).Id, ticket.Id, "modificó la fecha y hora del ticket.");
                            ticket.FechaCreacion = fechaHora;
                            break;
                        case "modificarAsignados":
                            modificarSupporters();
                            break;
                    }
                }

                bool exito = datosTicket.ModificarTicket(ticket);
                if (!exito)
                {
                    Modal.Mostrar(this, "Error", "No se pudo modificar el ticket. Por favor, inténtelo más tarde.", "error");
                    return false;
                }
                Modal.Mostrar(this, "Éxito", "El ticket se ha modificado correctamente.", "exito");
                Response.Redirect("TKT-" + id, true);
                return true;
            }
            catch (Exception Ex)
            {
                Modal.Mostrar(this, "Error", "No se pudo modificar el ticket. Por favor, inténtelo más tarde.", "error");
                return false;
            }
        }

        private void modificarSupporters()
        {
            List<int> idsSeleccionados = lstAsignados.Items.Cast<ListItem>().Select(item => int.Parse(item.Value)).ToList();
            TicketDatos ticketDatos = new TicketDatos();
            UsuarioDatos usuarioDatos = new UsuarioDatos();
            CommitDatos commitDatos = new CommitDatos();

            // Primero recorro los seleccionados, y los comparo con la lista actual de colaboradores, si no están en la lista actual, añadirlos.
            foreach (int id in idsSeleccionados)
            {
                if (!ticket.IdColaboradores.Contains(id))
                {
                    ticket.IdColaboradores.Add(id);
                    ticketDatos.AgregarColaborador(ticket.Id, id);
                    commitDatos.registrarLog(((Usuario)UsuarioDatos.UsuarioActual(Session["Usuario"])).Id, ticket.Id, "asignó a '" + usuarioDatos.GetUsuarioDTO(id).Nombre + "' como colaborador del ticket.");
                }
            }

            // Luego analizo la lista actual de colaboradores, si no estan en la lista de seleccionados, removerlo.
            List<int> colaboradoresActuales = new List<int>(ticket.IdColaboradores);
            foreach (int id in colaboradoresActuales)
            {
                if (!idsSeleccionados.Contains(id))
                {
                    ticket.IdColaboradores.Remove(id);
                    ticketDatos.QuitarColaborador(ticket.Id, id);
                    commitDatos.registrarLog(((Usuario)UsuarioDatos.UsuarioActual(Session["Usuario"])).Id, ticket.Id, "removió a '" + usuarioDatos.GetUsuarioDTO(id).Nombre + "' como colaborador del ticket.");
                }
            }
        }

        protected void btnAddSupport_Click(object sender, EventArgs e)
        {
            if (lstDisponibles.SelectedItem == null)
                return;

            string nombreSeleccionado = lstDisponibles.SelectedItem.Text;
            string idSeleccionado = lstDisponibles.SelectedValue;

            lstAsignados.Items.Add(new ListItem(nombreSeleccionado, idSeleccionado));
            lstDisponibles.Items.Remove(lstDisponibles.Items.FindByValue(idSeleccionado));
            //ticket.IdColaboradores.Add(idSeleccionado);
        }

        protected void btnRemoveSupport_Click(object sender, EventArgs e)
        {
            if (lstAsignados.SelectedItem == null)
                return;

            string nombreSeleccionado = lstAsignados.SelectedItem.Text;
            string idSeleccionado = lstAsignados.SelectedValue;

            lstDisponibles.Items.Add(new ListItem(nombreSeleccionado, idSeleccionado));
            lstAsignados.Items.Remove(lstAsignados.Items.FindByValue(idSeleccionado));

            //ticket.IdColaboradores.Remove(idSeleccionado);
        }
    }
}