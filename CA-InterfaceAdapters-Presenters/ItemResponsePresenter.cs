using CA_ApplicationLayer;
using CA_InterfaceAdapters_Mappers.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Presenters
{

    public class ItemResponsePresenter<TEntity, TOutput> : IPresenterResponse<TEntity, ItemResponse<TEntity>>
    {
        public ItemResponse<TEntity> Present(TEntity response)
        {
            ItemResponse<TEntity> itemResponse = new ItemResponse<TEntity>();
            itemResponse.Item = response;
            itemResponse.IsSuccess = true;
            return itemResponse;
        }
    }
}
