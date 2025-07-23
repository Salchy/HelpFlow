using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public int Id { get; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public nivelUsuario TipoUsuario { get; set; }

        public enum nivelUsuario
        {
            Administrador = 0,
            Usuario = 1
        }

        public Usuario(int id, string nombre, string correo, int tipoUsuario)
        {
            Id = id;
            Nombre = nombre;
            Correo = correo;
            TipoUsuario = (nivelUsuario)tipoUsuario;
        }
    }
}
