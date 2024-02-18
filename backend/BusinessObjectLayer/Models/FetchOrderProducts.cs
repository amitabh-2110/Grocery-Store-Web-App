using BusinessObjectLayer.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Models
{
    public class FetchOrderProducts
    {
        public List<Orders> Products { get; set; }

        public List<ImageInfoOrders> ImageData { get; set; } 
    }
}
