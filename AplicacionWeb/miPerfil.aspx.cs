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
    public partial class miPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario actualUser = UsuarioDatos.UsuarioActual(Session["Usuario"]);

                txtUsuario.Text = actualUser.UserName;
                txtNombre.Text = actualUser.Nombre;
                txtCorreo.Text = actualUser.Correo;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            UsuarioDatos userDatos = new UsuarioDatos();

            string nueva = txtNuevaClave.Text;
            string confirmar = txtConfirmarClave.Text;

            if (string.IsNullOrEmpty(nueva) || string.IsNullOrEmpty(confirmar))
            {
                lblMensaje.Text = "Debes completar ambos campos.";
                lblMensaje.CssClass = "text-danger mt-3 d-block text-center fw-bold";
                return;
            }

            if (nueva != confirmar)
            {
                lblMensaje.Text = "Las contraseñas no coinciden.";
                lblMensaje.CssClass = "text-danger mt-3 d-block text-center fw-bold";
                return;
            }

            if (userDatos.updatePassword(UsuarioDatos.UsuarioActual(Session["Usuario"]).Id, nueva))
            {
                lblMensaje.Text = "Contraseña actualizada correctamente.";
                lblMensaje.CssClass = "text-success mt-3 d-block text-center fw-bold";

                // Limpiar los campos
                txtNuevaClave.Text = "";
                txtConfirmarClave.Text = "";

            }
        }
    }
}