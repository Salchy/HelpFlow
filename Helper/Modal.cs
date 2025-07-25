using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace Helper
{
    public static void Mostrar(Page page, string titulo, string mensaje, string tipo)
    {
        // Escapar comillas simples para evitar errores de JS
        titulo = titulo.Replace("'", "\\'");
        mensaje = mensaje.Replace("'", "\\'");

        string script = $"mostrarModal('{titulo}', '{mensaje}', '{tipo}');";
        ScriptManager.RegisterStartupScript(page, page.GetType(), "mostrarModalScript", script, true);
    }
}
