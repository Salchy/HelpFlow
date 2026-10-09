    using AccesoDatos;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace AplicacionWeb
{
    public class BasePage : Page
    {
        protected Usuario UsuarioActual { get; private set; }

        // Sobreescribo OnInit de la clase Page para verificar la sesión del usuario antes de que se cargue la página.
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (Session["Usuario"] == null)
            {
                Redirigir("Login");
                return;
            }

            Usuario usuarioSesion = (Usuario)Session["Usuario"];

            UsuarioDatos usuarioDatos = new UsuarioDatos();
            Usuario usuarioActualizado = usuarioDatos.GetUsuario(usuarioSesion.Id);

            if (usuarioActualizado == null || !usuarioActualizado.Estado)
            {
                Session.Clear();
                Session.Abandon();

                Redirigir("Login");
                return;
            }

            Session["Usuario"] = usuarioActualizado;
            UsuarioActual = usuarioActualizado;
        }

        protected bool RequerirRol(Usuario.nivelUsuario rolRequerido)
        {
            if (UsuarioActual != null && UsuarioActual.TipoUsuario == rolRequerido)
            {
                return true;
            }

            Redirigir("AccesoDenegado");
            Context.ApplicationInstance.CompleteRequest();

            return false;
        }

        private void Redirigir(String url)
        {
            Response.Redirect("~/" + url + ".aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}