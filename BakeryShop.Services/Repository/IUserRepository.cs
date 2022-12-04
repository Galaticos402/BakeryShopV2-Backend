using BakeryShop.BusinessObject;
using BakeryShop.BusinessObject.DTOs.User;
using BakeryShop.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Repository
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByCredentials(string username, string password);
        IEnumerable<ApplicationUser> GetAll();
        Task<ApplicationUser> Create(UserRegisterModel user);
        Task<ApplicationUser> Update(ApplicationUser user);

        Task<ApplicationUser> GetById(Guid userID);
    }
}
