using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer.Common.IPresenterFactory
{
    public interface IPresenterGetAll<TEntity, TOutput>
    {
        public IEnumerable<TOutput> PresentGetAll(IEnumerable<TEntity> entities);
    }
}
