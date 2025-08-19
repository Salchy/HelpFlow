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
            if (!IsPostBack)
            {
                if (!UsuarioDatos.SesionActiva(Session["Usuario"]))
                {
                    Response.Redirect("Login.aspx", false);
                    return;
                }

                // Obtener el nivel de usuario del usuario
                Usuario usuarioActual = UsuarioDatos.UsuarioActual(Session["Usuario"]);
                if (usuarioActual == null)
                {
                    Response.Redirect("Login.aspx", false);
                    return;
                }

                // Mostrar opciones según nivel
                panelAdmin.Visible = ((int)usuarioActual.TipoUsuario == 0); // Admin
                panelUsuario.Visible = ((int)usuarioActual.TipoUsuario == 1); // Usuario
                //panelCerrarSesion.Visible = true;
            }
        }

        protected void MostrarModal(string titulo, string mensaje, string tipo)
        {
            string script = $"mostrarModal('{titulo}', '{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", script, true);
        }
    }
}