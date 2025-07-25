using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CommitDTO
    {
        public int Id { get; set; }
        public int IdAutor { get; set; }
        public String AutorNombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Mensaje { get; set; }
        public int IdTicketRelacionado { get; set; } // ID del ticket relacionado
        public bool TipoCommit { get; set; } // 0 es interno (Nota interna), 1 es público (al cliente)
    }
}
