using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using CA_InterfaceAdapters_Mappers.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Presenters
{

    public class LstItemPaginationResponsePresenter<T, TOutput> : ILstPagPresenterResponse<T, LstItemResponse<T>>
    {
        public LstItemResponse<T> Present(ItemsPaginatorEntity<T> response)
        {
            LstItemResponse<T> lstItemResponse = new LstItemResponse<T>();
            Pagination pagination = new Pagination()
            {
                PageIndex = response.PageIndex,
                PageSize = response.PageSize,
                TotalRows = response.TotalRows
            };
            lstItemResponse.LstItem = response.lstItem;
            lstItemResponse.IsSuccess = true;
            lstItemResponse.Pagination = pagination;
            return lstItemResponse;
        }
    }
}
