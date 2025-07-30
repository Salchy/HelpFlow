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
                    lblFecha.Text = ticket.FechaCreacion.ToString("dd/MM/yyyy");
                    MostrarEstado(ticket.Estado.Id);
                    cargarColaboradores(ticketID);
                    cargarCommits(ticketID);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

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

        private void cargarColaboradores(int IdTicket)
        {
            try
            {
                TicketDatos ticketDatos = new TicketDatos();
                Colaboradores = ticketDatos.ObtenerColaboradores(IdTicket);
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

        private void bindearDatos()
        {
            rptCommits.DataSource = null;
            rptCommits.DataSource = ListaCommits;
            rptCommits.DataBind();
            pnlSinCommits.Visible = (rptCommits.Items.Count == 0);
        }

        private void cargarCommits(int ticketID)
        {
            CommitDatos commitDatos = new CommitDatos();

            try
            {
                ListaCommits = commitDatos.GetTicketCommitsDTOs(ticketID);
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

        private bool obtenerTipoCommit () // True: nota interna, False: al cliente
        {
            if (!rbInterno.Checked) // Si no está marcado el radio de nota interna, entonces debe ser al cliente
            {
                return rbCliente.Checked ? false : throw new Exception("Debe seleccionar un tipo de commit (Público o Interno).");
            }
            else
            {
                return true; // Es nota interna
            }
        }

        private bool RegistrarCommit(string commitMsg, bool typeCommit)
        {
            CommitDatos commitDatos = new CommitDatos();
            Usuario usuario = UsuarioDatos.UsuarioActual(Session["Usuario"]);

            CommitDTO commit = new CommitDTO
            {
                IdAutor = usuario.Id,
                AutorNombre = usuario.Nombre,
                Mensaje = commitMsg,
                IdTicketRelacionado = (int)ViewState["TicketID"],
                TipoCommit = typeCommit
            };

            bool Success = commitDatos.InsertCommit(commit);
            if (!Success)
            {
                throw new Exception("Error al registrar el commit en la base de datos.");
            }
            ListaCommits.Add(commit);
            bindearDatos();
            Modal.Mostrar(this, "Éxito", "Commit registrado correctamente.", "success");
            return Success;
        }
    }
}