using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Commit
    {
        public int Id { get; }
        public Ticket TicketRelacionado { get; set; }
        public Usuario Autor { get; set; }
        public DateTime Fecha { get; set; }
        public string Mensaje { get; set; }
        public int TipoCommit { get; set; } // 0 es interno (Nota interna), 1 es público (al cliente)
    }
}