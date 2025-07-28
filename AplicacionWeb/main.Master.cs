using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AccesoDatos;

namespace AplicacionWeb
{
    public partial class main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioDatos userDatos = new UsuarioDatos();
            if (userDatos.SesionActiva)
        }

        protected void MostrarModal(string titulo, string mensaje, string tipo)
        {
            string script = $"mostrarModal('{titulo}', '{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarModal", script, true);
        }
    }
}