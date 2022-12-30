using BakeryShop.BusinessObject;
using BakeryShop.BusinessObject.Constants;
using BakeryShop.BusinessObject.DTOs.User;
using BakeryShop.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Repository.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly BakeryShopContext _bakeryShopContext;
        private readonly IdentityContext _identityContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public UserRepository(BakeryShopContext bakeryShopContext, IdentityContext identityContext, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _bakeryShopContext = bakeryShopContext;
            _identityContext = identityContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<ApplicationUser> Create(UserRegisterModel user)
        {
            if (!await _roleManager.RoleExistsAsync(RoleConstants.ADMIN))
            {
                await _roleManager.CreateAsync(new ApplicationRole(RoleConstants.ADMIN));
            }
            if (!await _roleManager.RoleExistsAsync(RoleConstants.CUSTOMERS))
            {
                await _roleManager.CreateAsync(new ApplicationRole(RoleConstants.CUSTOMERS));
            }
            var appUser = new ApplicationUser
            {
                UserName = user.Username,
                Email = user.Email,
                HouseNumber = user.HouseNumber,
                District = user.District,
                City = user.City,
                Street = user.Street,
                PhoneNumber = user.PhoneNumber,
            };
            /*Check if a user with the same username has existed in the database*/
            var UserExisted = await _userManager.FindByNameAsync(user.Username);
            if(UserExisted != null)
            {
                return null;
            }
            var res = await _userManager.CreateAsync(appUser, user.Password);
            await _userManager.AddToRoleAsync(appUser, RoleConstants.CUSTOMERS);
            if (res.Succeeded)
            {
                
               /* await _identityContext.SaveChangesAsync();*/
                var id =  _userManager.FindByNameAsync(user.Username).Result.Id;
                var guid = new Guid(id);
                var addToApplicationDb = await _bakeryShopContext.Users.AddAsync(new User
                {
                    UserID = guid
                });
                
                if (addToApplicationDb != null)
                {
                    await _bakeryShopContext.SaveChangesAsync();
                    return new ApplicationUser
                    {
                        UserName = user.Username,
                        Email = user.Email,
                        HouseNumber = user.HouseNumber,
                        District = user.District,
                        City = user.City,
                        PhoneNumber = user.PhoneNumber
                    };
                }
            }
            return null;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetById(Guid userID)
        {
            return await _identityContext.Users.FindAsync(userID);
        }

        public async Task<ApplicationUser> GetUserByCredentials(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return null;
            var Ismatch = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password); 
            if(Ismatch != PasswordVerificationResult.Success)
            {
                return null;
            }
            else
            {
                var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
                if(result != PasswordVerificationResult.Success)
                {
                    return null;
                }
                return user;
            }
            
            
        }

        public Task<ApplicationUser> Update(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
    }
}
