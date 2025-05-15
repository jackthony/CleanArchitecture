using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_EntrerpriseLayer
{
    public class EMP_DietaEntity
    {
        public string sRUC { get; set; }
        public int nTipoCargo { get; set; }
        public double mDieta { get; set; }
        public EMP_DietaEntity()
        {
            
        }
        public EMP_DietaEntity(string sRUC, int nTipoCargo, double mDieta)
        {
            this.sRUC = sRUC;
            this.nTipoCargo = nTipoCargo;
            this.mDieta = mDieta;
        }
    }
}
