using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Contracts
{
    public abstract class BaseResponse : BaseRequest
    {
        public bool IsSuccess { get; set; } = false;
        public List<string> LstError { get; set; } = new List<string>();
    }
}
