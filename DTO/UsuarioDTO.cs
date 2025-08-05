using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public nivelUsuario TipoUsuario { get; set; }
        public bool Estado { get; set; } = true;
        public enum nivelUsuario
        {
            Administrador = 0,
            Usuario = 1
        }

        public UsuarioDTO()
        {
            
        }

        public UsuarioDTO(int id, string userName, string nombre, string correo, int tipoUsuario, bool estado = true)
        {
            Id = id;
            UserName = userName;
            Nombre = nombre;
            Correo = correo;
            TipoUsuario = (nivelUsuario)tipoUsuario;
            Estado = estado;
        }
    }
}
