using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace AplicacionWeb
{
    public static class Modal
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="titulo"></param>
        /// <param name="mensaje"></param>
        /// <param name="tipo">error, exito, info, advertencia</param>

        public static void Mostrar(Page page, string titulo, string mensaje, string tipo)
        {
            // Escapar comillas simples para evitar errores de JS
            titulo = titulo.Replace("'", "\\'");
            mensaje = mensaje.Replace("'", "\\'");

            string script = $"mostrarModal('{titulo}', '{mensaje}', '{tipo}');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "mostrarModalScript", script, true);
        }
    }
}