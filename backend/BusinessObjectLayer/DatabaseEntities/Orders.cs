using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.DatabaseEntities
{
    public class Orders
    {
        public Guid OrderId { get; set; }

        public string Email { get; set; }

        public Guid ProductId { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime DateOfOrder { get; set; }
    }
}
