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
        public String Autor { get; set; }
        public DateTime Fecha { get; set; }
        public string Mensaje { get; set; }
        public bool TipoCommit { get; set; } // 0 es interno (Nota interna), 1 es público (al cliente)
    }
}
