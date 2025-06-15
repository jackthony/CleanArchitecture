using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_EntrerpriseLayer
{
    public class EMP_EmpresaEntity
    {
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
        public int nNumeroMiembros { get; set; }
        public bool bRegistradoMercadoValores { get; set; }
        public bool bActivo { get; set; }
        public DateTime dtFechaRegistro { get; set; }
        public int nUsuarioRegistro { get; set; }
        public DateTime dtFechaModificacion { get; set; }
        public int nUsuarioModificacion { get; set; }
    }
}
