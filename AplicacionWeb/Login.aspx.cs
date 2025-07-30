using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccesoDatos;
using Dominio;
using AplicacionWeb.Helpers;

namespace AplicacionWeb
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
            {
                Usuario user = (Usuario)Session["Usuario"];
                switch ((int)user.TipoUsuario)
                {
                    case (int)Usuario.nivelUsuario.Administrador:
                        Response.Redirect("main.aspx", false);
                        break;
                    case (int)Usuario.nivelUsuario.Usuario:
                        Response.Redirect("misTickets.aspx", false);
                        break;
                    default:
                        Response.Redirect("misTickets.aspx", false);
                        break;
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsuario.Text.Trim();
            string password = txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Modal.Mostrar(this, "Error", "Por favor, ingrese su usuario y contraseña.", "error");
                return;
            }
            UsuarioDatos UserDB = new UsuarioDatos();
            Usuario UserAttempLogin = UserDB.GetUsuario(username, password);
            if (UserAttempLogin == null)
            {
                Modal.Mostrar(this, "Error", "Usuario o contraseña incorrectos.", "error");
                return;
            }
            if (!UserAttempLogin.Estado)
            {
                Response.Write("<script>alert('El usuario está deshabilitado.');</script>");
                return;
            }
            Session["Usuario"] = UserAttempLogin;
            switch ((int)UserAttempLogin.TipoUsuario)
            {
                case (int)Usuario.nivelUsuario.Administrador:
                    Response.Redirect("main.aspx", false);
                    break;
                case (int)Usuario.nivelUsuario.Usuario:
                    Response.Redirect("misTickets.aspx", false);
                    break;
                default:
                    Response.Redirect("misTickets.aspx", false);
                    break;
            }
        }
    }
}