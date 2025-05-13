using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Contracts
{
    public class ItemResponse<T> : BaseResponse
    {
        public T Item { get; set; }
    }
}
