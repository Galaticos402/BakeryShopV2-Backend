﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base()
        {

        }
        public ApplicationRole(string role) : base(role)
        {
            
        }
    }
}
