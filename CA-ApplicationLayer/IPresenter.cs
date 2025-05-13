using CA_EntrerpriseLayer;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_ApplicationLayer
{
    public interface IPresenter<TEntity, TOutput>
    {
        public IEnumerable<TOutput> Present(IEnumerable<TEntity> entities);
    }

    public interface ILstPresenterResponse<TResponse , TOutput>
    {
        public TOutput Present(IEnumerable<TResponse> response);
    }

    public interface IPresenterResponse<Tentity, TOutput>
    {
        public TOutput Present(Tentity response);
    }

    public interface ILstPagPresenterResponse<Tentity, TOutput>
    {
        public TOutput Present(ItemsPaginatorEntity<Tentity> response);
    }
}
