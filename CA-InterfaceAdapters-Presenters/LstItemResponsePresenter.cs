using CA_ApplicationLayer;
using CA_InterfaceAdapters_Mappers.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Presenters
{

    public class LstItemResponsePresenter<T, TOutput> : ILstPresenterResponse<T, LstItemResponse<T>>
    {
        public LstItemResponse<T> Present(IEnumerable<T> response)
        {
            LstItemResponse<T> lstItemResponse = new LstItemResponse<T>();
            lstItemResponse.LstItem = response;
            lstItemResponse.IsSuccess = true;
            return lstItemResponse;
        }
    }
}
