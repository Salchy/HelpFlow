using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccesoDatos;
using Dominio;

namespace AplicacionWeb
{
    public partial class main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuarioActual = Session["Usuario"] as Usuario;

            // Mostrar las opciones según el rol.
            panelAdmin.Visible = usuarioActual != null && (int)usuarioActual.TipoUsuario == 0;      // Admin
            panelUsuario.Visible = usuarioActual != null && (int)usuarioActual.TipoUsuario == 1;    // Usuario
        }

        protected void MostrarModal(string titulo, string mensaje, string tipo)
        {
            string script = $"mostrarModal('{titulo}', '{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", script, true);
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect("~/Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}