using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UsuarioColaboradorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }

        public UsuarioColaboradorDTO(int id, string nombre, string correo)
        {
            Id = id;
            Nombre = nombre;
            Correo = correo;
        }
    }
}
