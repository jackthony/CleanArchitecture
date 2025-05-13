using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_EntrerpriseLayer
{
    public class DistritosModel
    {
        [Key]
        public string sCode { get; set; }
        public string sName { get; set; }
        public string sProvinceCode { get; set; }
    }
}
