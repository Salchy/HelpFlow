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

namespace AplicacionWeb
{
    public partial class ticket : System.Web.UI.Page
    {
        private List<CommitDTO> Commits = new List<CommitDTO>();
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
                    ViewState["TicketID"] = ticketID;
                    Ticket ticket = ticketDatos.ObtenerTicket(ticketID);

                    tituloTicket.InnerText = "Ticket #" + ticketID;

                    lblTitulo.Text = ticket.Asunto;
                    lblSolicitante.Text = ticket.UsuarioCreador.Nombre;
                    lblDescripcion.Text = ticket.Descripcion;
                    lblEstado.Text = ticket.Estado.NombreEstado;
                    //lblUsuarioAsignado.Text = ticket.UsuarioAsignado.Nombre;
                    lblFecha.Text = ticket.FechaCreacion.ToString("dd/MM/yyyy");
                    MostrarEstado(ticket.Estado.Id);
                    cargarCommits(ticketID);
                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        private void MostrarEstado(int IdEstado)
        {
            switch (IdEstado)
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

        private void bindearDatos(List<CommitDTO> commits)
        {
            rptCommits.DataSource = null;
            rptCommits.DataSource = commits;
            rptCommits.DataBind();
            pnlSinCommits.Visible = (rptCommits.Items.Count == 0);
        }

        private void cargarCommits(int ticketID)
        {
            CommitDatos commitDatos = new CommitDatos();

            try
            {
                Commits = commitDatos.GetTicketCommitsDTOs(ticketID);
                bindearDatos(Commits);
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
        private bool RegistrarCommit(string commitMsg)
        {
            CommitDatos commitDatos = new CommitDatos();

            CommitDTO commit = new CommitDTO
            {
                IdAutor = 1, // TODO: Asignar el ID del usuario actual
                AutorNombre = "Usuario de Prueba", // TODO: Asignar el nombre del usuario actual (Se va a tomar el id del autor de sesion)
                Mensaje = commitMsg,
                IdTicketRelacionado = (int)ViewState["TicketID"],
                TipoCommit = false // TODO: Asignar el tipo de commit (interno o público)
            };

            bool Success = commitDatos.InsertCommit(commit);
            if (!Success)
            {
                throw new Exception("Error al registrar el commit en la base de datos.");
            }
            Commits.Add(commit);
            bindearDatos(Commits);
            return Success;
        }
    }
}