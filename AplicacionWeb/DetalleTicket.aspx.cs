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
    public partial class DetalleTicket : System.Web.UI.Page
    {
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
                int IdTicket = Request.QueryString["id"] != null ? int.Parse(Request.QueryString["id"]) : -1;
                if (IdTicket == -1)
                {
                    Modal.Mostrar(this, "Error", "No se ha proporcionado un ID de ticket válido.", "error");
                    return;
                }
                try
                {
                    TicketActual = ticketDatos.ObtenerTicket(IdTicket);

                    tituloTicket.InnerText = "Ticket #" + IdTicket;

                    lblTitulo.Text = TicketActual.Asunto;
                    lblSolicitante.Text = TicketActual.UsuarioCreador.Nombre;
                    lblDescripcion.Text = TicketActual.Descripcion;
                    lblEstado.Text = TicketActual.Estado.NombreEstado;
                    lblFecha.Text = TicketActual.FechaCreacion.ToString("dd/MM/yyyy");
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

        private void MostrarEstado()
        {
            switch (TicketActual.Estado.Id)
            {
                case 0:
                    lblEstado.CssClass = "badge bg-primary";
                    break;

                case 1:
                    lblEstado.CssClass = "badge bg-warning text-dark";
                    break;

                case 2:
                    lblEstado.CssClass = "badge bg-success";
                    break;

                case 3:
                    lblEstado.CssClass = "badge bg-secondary";
                    break;

                default:
                    lblEstado.CssClass = "badge bg-dark";
                    break;
            }
        }

        private void cargarColaboradores()
        {
            try
            {
                TicketDatos ticketDatos = new TicketDatos();
                mostrarColaboradores(ticketDatos.ObtenerColaboradores(TicketActual.Id));
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

        private void cargarCommits()
        {
            CommitDatos commitDatos = new CommitDatos();

            try
            {
                rptCommits.DataSource = null;
                rptCommits.DataSource = commitDatos.GetTicketCommitsDTOs(TicketActual.Id, 5);
                rptCommits.DataBind();
                pnlSinCommits.Visible = (rptCommits.Items.Count == 0);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private bool RegistrarCommit(string commitMsg)
        {
            CommitDatos commitDatos = new CommitDatos();
            Usuario usuario = UsuarioDatos.UsuarioActual(Session["Usuario"]);

            CommitDTO commit = new CommitDTO
            {
                IdAutor = usuario.Id,
                AutorNombre = usuario.Nombre,
                Mensaje = commitMsg,
                IdTicketRelacionado = TicketActual.Id,
                TipoCommit = 4 // Es un commit de un cliente
            };

            bool Success = commitDatos.InsertCommit(commit);
            if (!Success)
            {
                throw new Exception("Error al registrar el commit en la base de datos.");
            }
            cargarCommits();

            Helper.notificarSupporters(TicketActual.Id, "Novedades Ticket - #" + TicketActual.Id, "Respuesta de " + usuario.Nombre + ": " + commitMsg);

            Modal.Mostrar(this, "Éxito", "Commit registrado correctamente.", "exito");
            txtMensaje.Text = "";
            return Success;
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
            try
            {
                RegistrarCommit(commitMsg);
            }
            catch (Exception Ex)
            {
                Modal.Mostrar(this, "Error", "No se pudo registrar el commit: " + Ex.Message, "error");
                return;
            }
        }
    }
}