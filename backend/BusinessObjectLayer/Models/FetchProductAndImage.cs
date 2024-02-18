using BusinessObjectLayer.DatabaseEntities;
using BusinessObjectLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Models
{
    public class FetchProductAndImage
    {
        public List<Products> Product { get; set; }

        public List<ImageInfo> ImageData { get; set; }
    }
}
