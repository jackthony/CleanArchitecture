using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Mappers.Contracts
{
    public abstract class BaseRequest
    {
        public Guid Ticket { get; set; } = Guid.NewGuid();
        public string ClientName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string ServerName { get; set; } = string.Empty;
        public int Resultado { get; set; } = int.MaxValue;
    }
}
