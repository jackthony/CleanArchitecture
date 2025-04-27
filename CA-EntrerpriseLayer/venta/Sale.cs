using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_EntrerpriseLayer.venta
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Total { get; set; }
        public List<Concept> Concepts { get; set; }

        public Sale(DateTime date, List<Concept> concepts)
        {
            Date = date;
            Concepts = concepts;
            Total = GetTotal();
        }

        public Sale(int id, DateTime date, List<Concept> concepts){
            Id = id;
            Date = date;
            Concepts = concepts;
            Total = GetTotal();
        }

        private decimal GetTotal()
            => Concepts.Sum(c => c.Price);

    }
}
