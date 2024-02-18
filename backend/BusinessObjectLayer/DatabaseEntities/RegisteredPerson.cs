using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjectLayer.DatabaseEntities
{
    public class RegisteredPerson
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "Maxixmum 50 character are allowed")]
        [RegularExpression(@"^[a-zA-Z]{0,50}$", ErrorMessage = "Only alphabetical characters are allowed")]
        [Required]
        public string FullName { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Only 10 digits are allowed")]
        [Required]
        public string PhoneNumber { get; set; }

        public string Role { get; set; }

        [RegularExpression(@"^(?=.*[!@#$%&*()-+=^])(?=.*[0-9])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Password must contain atleast 8 characters long and contain atleast one special symbol, one number and one alphabet")]
        [Required]
        public string Password { get; set; }
    }
}
