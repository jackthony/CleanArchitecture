using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Models
{
    public class EMP_EmpresaModel
    {
        [Key]
        public int nIdEmpresa { get; set; }
        public string sRuc { get; set; }
        public string sRazonSocial { get; set; }
        public int nIdProponente { get; set; }
        public int nIdRubroNegocio { get; set; }
        public string sIdDepartamento { get; set; }
        public string sIdProvincia { get; set; }
        public string sIdDistrito { get; set; }
        public string sDireccion { get; set; }
        public string sComentario { get; set; }
        public decimal mIngresosUltimoAnio { get; set; }
        public decimal mUtilidadUltimoAnio { get; set; }
        public decimal mConformacionCapitalSocial { get; set; }

        public int nNumeroMiembros { get; }
        public bool bRegistradoMercadoValores { get; set; }
        public bool bActivo { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public string sUsuarioRegistro { get; set; }
        public DateTime? dtFechaModificacion { get; set; }
        public string? sUsuarioModificacion { get; set; }
    }
}
