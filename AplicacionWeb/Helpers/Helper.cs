using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AccesoDatos;
using DTO;

namespace AplicacionWeb.Helpers
{
    public static class Helper
    {
        public static void notificarSupporters(int ticketID, string asunto, string msg)
        {
            UsuarioDatos usuarioDatos = new UsuarioDatos();

            try
            {
                foreach (UsuarioDTO supporter in usuarioDatos.GetSupporters())
                {
                    MailHelper.SendEmail(supporter.Correo, asunto, msg, supporter.Nombre, ticketID.ToString());
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}