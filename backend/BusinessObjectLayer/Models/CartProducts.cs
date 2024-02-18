using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Models
{
    public class CartProducts
    {
        public int CartQuantity { get; set; }

        public decimal CartPrice { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ProductDescription { get; set; } = string.Empty;

        public decimal ProductPrice { get; set; }

        public int AvailableQuantity { get; set; }
    }
}
