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
        private Ticket TicketActual;
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
                ListaCommits = commitDatos.GetTicketCommitsDTOs(TicketActual.Id, 4);
                bindearDatos();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void bindearDatos()
        {
            rptCommits.DataSource = null;
            rptCommits.DataSource = ListaCommits;
            rptCommits.DataBind();
            pnlSinCommits.Visible = (rptCommits.Items.Count == 0);
        }

        protected void btnRegistrarCommit_Click(object sender, EventArgs e)
        {

        }
    }
}