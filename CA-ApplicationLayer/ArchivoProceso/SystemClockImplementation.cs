using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.ArchivoProceso
{
    public class SystemClockImplementation : IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
