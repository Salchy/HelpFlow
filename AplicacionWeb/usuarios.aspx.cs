using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccesoDatos;
using Dominio;
using DTO;

namespace AplicacionWeb
{
    public partial class usuarios : System.Web.UI.Page
    {
        private List<UsuarioDTO> listaUsuarios = new List<UsuarioDTO>();
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioDatos usuarioDatos = new UsuarioDatos();
            if (!IsPostBack)
            {
                listaUsuarios = usuarioDatos.GetUsuarios();
                bindearDatos();
            }
        }
        private void bindearDatos()
        {
            gvUsuarios.DataSource = null;
            gvUsuarios.DataSource = listaUsuarios;
            gvUsuarios.DataBind();
            //pnlSinCommits.Visible = (rptCommits.Items.Count == 0);
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {

        }

        protected void btnResetPass_Click(object sender, EventArgs e)
        {

        }

        protected void btnNuevoUsuario_Click(object sender, EventArgs e)
        {

        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {

        }
    }
}