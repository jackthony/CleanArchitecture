using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Presenters
{
    public class BeerDetailPresenter : IPresenter<Beer, BeerDetailViewModel>
    {
        public IEnumerable<BeerDetailViewModel> Present(IEnumerable<Beer> beers)
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
