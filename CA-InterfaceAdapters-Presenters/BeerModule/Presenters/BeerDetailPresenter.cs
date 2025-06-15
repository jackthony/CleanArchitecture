using CA_ApplicationLayer.Common.IPresenterFactory;
using CA_EntrerpriseLayer.BeerModule;
using CA_InterfaceAdapters_Presenters.BeerModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Presenters.BeerModule.BeerDetail
{
    public class BeerDetailPresenter : IPresenterGetAll<Beer, BeerDetailViewModel>
    {
        public IEnumerable<BeerDetailViewModel> PresentGetAll(IEnumerable<Beer> beers)
            => beers.Select(b => new BeerDetailViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Alcohol = b.Alcohol + " %",
                Color = b.IsStrongBeer() ? "#ff0045" : "green",
                Style = b.Style,
                Message = b.IsStrongBeer() ? "Cerveza fuerte" : ""
            });
    }
}
