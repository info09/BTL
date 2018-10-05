using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class OrderViewModel
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public string ShipName { get; set; }

        public string ShipAddress { get; set; }

        public string ShipMobile { get; set; }

        public string ShipEmail { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
