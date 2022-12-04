using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {

        }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string City { get; set; }
    }
}
