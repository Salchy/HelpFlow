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

    public class TicketCreacionDTO
    {
        public int Id { get; set; } = -1;
        public int IdCreador { get; set; }
        public int IdSubCategoria { get; set; }
        public int IdEstado { get; set; } = 1; // Por defecto, el estado es "Solicitado"
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public List<int> IdColaboradores { get; set; } = new List<int>();
    }
}