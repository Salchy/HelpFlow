using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public string Asunto { get; set; }
        public string UsuarioCreador { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Colaboradores { get; set; }
    }
}