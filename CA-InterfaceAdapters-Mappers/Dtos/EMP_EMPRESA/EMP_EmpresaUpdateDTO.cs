using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Dtos.EMP_EMPRESA
{
    public class EMP_EmpresaUpdateDTO
    {
        public int nIdEmpresa { get; set; }
        public int nIdProponente { get; set; }
        public int nIdRubroNegocio { get; set; }
        public int sIdDepartamento { get; set; }
        public int sIdProvincia { get; set; }
        public int sIdDistrito { get; set; }
        public string sDireccion { get; set; }
        public string sComentario { get; set; }
        public decimal mIngresosUltimoAnio { get; set; }
        public decimal mUtilidadUltimoAnio { get; set; }
        public decimal mConformacionCapitalSocial { get; set; }
        public int nNumeroMiembros { get; set; }
        public bool bRegistradoMercadoValores { get; set; }
        public bool bActivo { get; set; }
        public string sUsuarioModificacion { get; set; }
    }
}
