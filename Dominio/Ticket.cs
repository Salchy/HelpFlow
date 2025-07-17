using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Ticket
    {
        public int Id { get; }
        public Usuario UsuarioCreador { get; set; }
        public string Asunto { get; set; }
        public Estado Estado { get; set; }
        public DateTime  FechaCreacion { get; set; }
        public string Descripcion { get; set; }
        public List<Usuario> Colaboradores { get; }
        public List<Commit> Commits { get; } // Quizá no haga falta, ya que puedo obtenerlo sólo cuando haga falta, no voy a andar cargando todos los commits cuando creo un objeto Ticket.
    }
}