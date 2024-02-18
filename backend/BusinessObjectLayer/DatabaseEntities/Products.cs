using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.DatabaseEntities
{
    public class Products
    {
        public Guid? ProductId { get; set; }

        [StringLength(100, ErrorMessage = "Maximum 100 characters are allowed")]
        [RegularExpression("^[a-zA-Z ]{0,100}$", ErrorMessage = "Only characters are allowed")]
        [Required]
        public string ProductName { get; set; }

        [StringLength(255, ErrorMessage = "Maximum 255 characters are allowed")]
        [RegularExpression("^[a-zA-Z0-9, ]{0,255}$", ErrorMessage = "Only alpha-numeric characters are allowed")]
        [Required]
        public string Description { get; set; }

        [StringLength(100, ErrorMessage = "Maximum 100 characters are allowed")]
        [RegularExpression("^[a-zA-Z]{0,100}$", ErrorMessage = "Only alpha-numeric characters are allowed")]
        [Required]
        public string Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Available quantity should be atleast 1")]
        [Required]
        public int AvailableQuantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Enter valid price")]
        [Required]
        public decimal Price { get; set; }
    }
}
