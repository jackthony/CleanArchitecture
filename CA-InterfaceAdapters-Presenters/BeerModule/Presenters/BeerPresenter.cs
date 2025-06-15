using CA_ApplicationLayer.Common.IPresenterFactory;
using CA_EntrerpriseLayer.BeerModule;
using CA_InterfaceAdapters_Presenters.BeerModule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_InterfaceAdapters_Presenters.BeerModule.Presenters
{
    public class BeerPresenter : IPresenterGetAll<BeerEntity, BeerViewModel>
    {
        public IEnumerable<BeerViewModel> PresentGetAll(IEnumerable<BeerEntity> beers)
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
