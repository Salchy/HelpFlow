using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using AccesoDatos;
using DTO;
using System.Collections;
using AplicacionWeb.Helpers;

namespace AplicacionWeb
{
    public partial class ticket : System.Web.UI.Page
    {
        private List<UsuarioColaboradorDTO> Colaboradores = new List<UsuarioColaboradorDTO>();
        private List<CommitDTO> ListaCommits
        {
            get
            {
                if (Session["Commits"] == null)
                    Session["Commits"] = new List<CommitDTO>();
                return (List<CommitDTO>)Session["Commits"];
            }
            set
            {
                Session["Commits"] = value;
            }
        }

        private Ticket TicketActual
        {
            get
            {
                if (Session["Ticket"] == null)
                    Session["Ticket"] = new Ticket();
                return (Ticket)Session["Ticket"];
            }
            set
            {
                Session["Ticket"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            TicketDatos ticketDatos = new TicketDatos();

            if (!IsPostBack)
            {
                string idRuta = Page.RouteData.Values["id"] as string;

                if (string.IsNullOrEmpty(idRuta))
                {
                    Response.Redirect("main.aspx");
                    return;
                }

                try
                {
                    int ticketID = int.Parse(idRuta);
                    TicketActual = ticketDatos.ObtenerTicket(ticketID);
                    if (TicketActual.Id == -1)
                    {
                        // TODO: Hacer que muestre en la página de ticket, que el ticket no existe.
                        Modal.Mostrar(this, "Error", "El ticket no existe o no está disponible.", "error");
                        Response.Redirect("tickets.aspx");
                        return;
                    }
                    tituloTicket.InnerText = "Ticket #" + ticketID;

                    ddlEstado.Items.Add(new ListItem("Solicitado", "0"));
                    ddlEstado.Items.Add(new ListItem("En Progreso", "1"));
                    ddlEstado.Items.Add(new ListItem("Resuelto", "2"));
                    ddlEstado.Items.Add(new ListItem("Cerrado", "3"));

                    lblTitulo.Text = TicketActual.Asunto;
                    lblSolicitante.Text = TicketActual.UsuarioCreador.Nombre;
                    lblDescripcion.Text = TicketActual.Descripcion;
                    lblFecha.Text = TicketActual.FechaCreacion.ToString("dd/MM/yyyy HH:mm:ss");
                    MostrarEstado();
                    cargarColaboradores();
                    cargarCommits();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void cargarColaboradores()
        {
            try
            {
                TicketDatos ticketDatos = new TicketDatos();
                Colaboradores = ticketDatos.ObtenerColaboradores(TicketActual.Id);
                mostrarColaboradores(Colaboradores);
            }
            catch (Exception Ex)
            {

                throw Ex;
            }
        }

        private void mostrarColaboradores(List<UsuarioColaboradorDTO> colaboradores)
        {
            if (colaboradores == null || colaboradores.Count == 0)
            {
                lblUsuarioAsignado.Text = "No hay colaboradores asignados.";
                return;
            }
            lblUsuarioAsignado.Text = string.Join(", ", colaboradores.Select(c => c.Nombre));
        }

        private void MostrarEstado()
        {
            ddlEstado.SelectedValue = TicketActual.Estado.Id.ToString();
            string stringClass = "";
            switch (TicketActual.Estado.Id)
            {
                case 0:
                    stringClass = "form-select bg-primary text-white";
                    break;

                case 1:
                    stringClass = "form-select bg-warning text-dark";
                    break;

                case 2:
                    stringClass = "form-select bg-success text-white";
                    break;

                case 3:
                    stringClass = "form-select bg-secondary text-white";
                    break;
            }
            ddlEstado.CssClass = stringClass;
        }

        private void bindearDatos()
        {
            rptCommits.DataSource = null;
            rptCommits.DataSource = ListaCommits;
            rptCommits.DataBind();
            pnlSinCommits.Visible = (rptCommits.Items.Count == 0);
        }

        // TODO: Hacer que se filtren los resultados, dependiendo si tiene presionado el boton de "Todo", "Comentarios" o "Actividad".
        private void cargarCommits()
        {
            CommitDatos commitDatos = new CommitDatos();

            try
            {
                ListaCommits = commitDatos.GetTicketCommitsDTOs(TicketActual.Id);
                bindearDatos();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        protected void btnRegistrarCommit_Click(object sender, EventArgs e)
        {
            string commitMsg = txtMensaje.Text;

            if (commitMsg.Length < 5)
            {
                Modal.Mostrar(this, "Error", "El mensaje del commit debe tener al menos 5 caracteres.", "error");
                return;
            }
            if (commitMsg.Length > 500)
            {
                Modal.Mostrar(this, "Error", "El mensaje del commit no puede exceder los 500 caracteres.", "error");
                return;
            }
            bool notaInterna = obtenerTipoCommit();
            try
            {
                RegistrarCommit(commitMsg, notaInterna);
            }
            catch (Exception Ex)
            {
                Modal.Mostrar(this, "Error", "No se pudo registrar el commit: " + Ex.Message, "error");
                return;
            }
        }

        private bool obtenerTipoCommit() // True: nota interna, False: al cliente
        {
            if (!rbInterno.Checked) // Si no está marcado el radio de nota interna, entonces debe ser al cliente
            {
                return rbCliente.Checked ? false : throw new Exception("Debe seleccionar un tipo de commit (Público o Interno).");
            }
            else
            {
                rbInterno.Checked = false;
                rbCliente.Checked = true;
                
                return true; // Es nota interna
            }
        }

        private bool RegistrarCommit(string commitMsg, bool esInterno)
        {
            CommitDatos commitDatos = new CommitDatos();
            Usuario usuario = UsuarioDatos.UsuarioActual(Session["Usuario"]);

            CommitDTO commit = new CommitDTO
            {
                IdAutor = usuario.Id,
                AutorNombre = usuario.Nombre,
                Mensaje = commitMsg,
                IdTicketRelacionado = TicketActual.Id,
                TipoCommit = esInterno ? (byte)1 : (byte)2
            };

            bool Success = commitDatos.InsertCommit(commit);
            if (!Success)
            {
                throw new Exception("Error al registrar el commit en la base de datos.");
            }
            ListaCommits.Add(commit);
            bindearDatos();
            if (!esInterno) // Si no es interno, notificar al cliente
            {
                MailHelper.SendEmail(TicketActual.UsuarioCreador.Correo, "Novedades Ticket - #" + TicketActual.Id, "Respuesta de " + usuario.Nombre + ": " + commitMsg, TicketActual.UsuarioCreador.Nombre, TicketActual.Id.ToString());
            }
            Modal.Mostrar(this, "Éxito", "Commit registrado correctamente.", "exito");
            txtMensaje.Text = "";
            return Success;
        }
        protected void modificarTicket_Click(object sender, EventArgs e)
        {
            LinkButton btnPresionado = (LinkButton)sender;

            Response.Redirect($"TicketForm.aspx?id={TicketActual.Id}&action={btnPresionado.ID}");
        }

        public void btnModificarTicket_Click(object sender, EventArgs e)
        {
            Response.Redirect($"TicketForm.aspx?id={TicketActual.Id}");
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommitDatos commitDatos = new CommitDatos();

            ListItem estado = ddlEstado.SelectedItem;

            // Registrar el log
            commitDatos.registrarLog(((Usuario)UsuarioDatos.UsuarioActual(Session["Usuario"])).Id, TicketActual.Id, "modificó el estado del ticket de '" + TicketActual.Estado.NombreEstado + "' a '" + estado.Text + "'.");

            // Modificar el estado del ticket
            TicketActual.Estado.Id = int.Parse(estado.Value);
            TicketActual.Estado.NombreEstado = estado.Text;

            TicketDatos ticketDatos = new TicketDatos();
            ticketDatos.ModificarEstado(TicketActual.Id, TicketActual.Estado.Id);     
 
            MailHelper.SendEmail(TicketActual.UsuarioCreador.Correo, "Novedades Ticket - #" + TicketActual.Id, "Se modificó el estado de tu ticket a " + estado.Text, TicketActual.UsuarioCreador.Nombre, TicketActual.Id.ToString());

            MostrarEstado();
            cargarCommits();
        }
    }
}