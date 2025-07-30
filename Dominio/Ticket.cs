using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Ticket
    {
        public int Id { get; } = -1; // Por defecto, se crea con ID -1, no es un ticket válido
        public Usuario UsuarioCreador { get; set; }
        public string Asunto { get; set; }
        public Estado Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string Descripcion { get; set; }
        public List<Usuario> Colaboradores { get; }
        public List<Commit> Commits { get; } // Quizá no haga falta, ya que puedo obtenerlo sólo cuando haga falta, no voy a andar cargando todos los commits cuando creo un objeto Ticket.

        // Posibles métodos:
        // AsignarUsuario, cambiar estado, 
        // Cosas que modifiquen el estado de un ticket en cuestión

        // Constructor
        public Ticket()
        {

        }
        public Ticket(int id, Usuario usuarioCreador, string asunto, Estado estado, DateTime fechaCreacion, DateTime fechaActualizacion, string descripcion)
        {
            Id = id;
            UsuarioCreador = usuarioCreador;
            Asunto = asunto;
            Estado = estado;
            FechaCreacion = fechaCreacion;
            FechaActualizacion = fechaActualizacion;
            Descripcion = descripcion;
            //Colaboradores = new List<Usuario>();
        }
    }
}