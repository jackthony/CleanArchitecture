using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Contracts
{
    public class LstItemResponse<T> : BaseResponse
    {
        public IEnumerable<T> LstItem { get; set; } = new List<T>();
        public Pagination Pagination { get; set; } = new Pagination();
    }
}
