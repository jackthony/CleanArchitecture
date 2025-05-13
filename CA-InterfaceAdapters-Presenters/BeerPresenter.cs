using CA_ApplicationLayer;
using CA_EntrerpriseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Presenters
{
    public class BeerPresenter : IPresenter<Beer, BeerViewModel>
    {
        public IEnumerable<BeerViewModel> Present(IEnumerable<Beer> beers)
        {
            return beers.Select(e => new BeerViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Alcohol = e.Alcohol + "%"
            });
        }
    }
}
