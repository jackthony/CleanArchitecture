using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_EntrerpriseLayer.BeerModule
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Style { get; set; }
        public decimal Alcohol { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.UtcNow;
        public string userCreated { get; set; } = "System"; // Default value, can be changed later
        public DateTime DateUpdate { get; set; } = DateTime.UtcNow;
        public string userUpdated { get; set; } = "System"; // Default value, can be changed later

        public bool IsStrongBeer() => Alcohol > 7.5m; 
    }
}
