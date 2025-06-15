using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Common.IMappersFactory
{
    public interface IMapperDtoToOutput<TDTO, TOutput>
    {
        public TOutput ToEntity(TDTO dto);
    }
}
