using BakeryShop.BusinessObject.DTOs.User;
using BakeryShop.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Service
{
    public interface IUserService
    {
        Task<ApplicationUser> Authenticate(UserLoginModel model);
        Task<string> GenerateToken(ApplicationUser model);
    }
}
