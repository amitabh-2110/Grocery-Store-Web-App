using BusinessObjectLayer.DatabaseEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.Models
{
    public class StoreProductAndImage
    {
        public Products Product { get; set; }

        public IFormFile ImageData { get; set; }
    }
}
