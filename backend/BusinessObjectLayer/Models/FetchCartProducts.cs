using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Models
{
    public class FetchCartProducts
    {
        public List<CartProducts> Products { get; set; }

        public List<ImageInfo> Images { get; set; }

        public string status = "ok";
    }
}
