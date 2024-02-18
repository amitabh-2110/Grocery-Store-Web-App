using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Models
{
    public class ImageInfoOrders
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public string ImageUrl { get; set; }
    }
}
