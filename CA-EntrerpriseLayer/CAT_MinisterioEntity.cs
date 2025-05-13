using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_EntrerpriseLayer
{
    public class CAT_MinisterioEntity
    {
        public int nIdMinisterio { get; set; }
        public string sNombreMinisterio { get; set; }
        public bool bActivo { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public string sUsuarioRegistro { get; set; }
        public DateTime dtFechaModificacion { get; set; }
        public string sUsuarioModificacion { get; set; }
    }
}