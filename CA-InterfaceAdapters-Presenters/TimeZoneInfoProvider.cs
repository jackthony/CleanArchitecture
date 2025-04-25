using CA_ApplicationLayer;
using CA_FrameworksDrivers_API.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Presenters
{
    public class TimeZoneInfoProvider : ITimeZoneInfoProvider
    {
        private readonly TimeZoneInfo _peruTimeZone;

        public TimeZoneInfoProvider(IOptions<TimeZoneSettings> options)
        {
            var tzId = options.Value.PeruTimeZoneId;
            _peruTimeZone = TimeZoneInfo.FindSystemTimeZoneById(tzId);
        }

        public DateTime GetCurrentTimeInZone()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _peruTimeZone);
        }
    }
}
