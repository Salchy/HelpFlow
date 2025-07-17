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
        public int TipoUsuario { get; set; }
    }
}
