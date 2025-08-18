using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class SubCategoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCategoria { get; set; } // Relación con la categoría padre
    }
}