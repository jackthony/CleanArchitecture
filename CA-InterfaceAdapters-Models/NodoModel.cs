using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Models
{
    public class NodoModel
    {
        public string Tipo { get; set; }  = string.Empty;
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public Guid? IdCarpeta { get; set; }
        public string? Extension { get; set; }
        public long? Peso { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
