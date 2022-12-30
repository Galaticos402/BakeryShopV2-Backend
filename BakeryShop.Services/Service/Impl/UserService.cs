using BakeryShop.BusinessObject;
using BakeryShop.BusinessObject.DTOs.User;
using BakeryShop.Data.Repository;
using BakeryShop.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(IUserRepository userRepository, IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<ApplicationUser> Authenticate(UserLoginModel model)
        {
            return await _userRepository.GetUserByCredentials(model.Username, model.Password);
        }

        public async Task<string> GenerateToken(ApplicationUser model)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            IEnumerable<string> roles = _userManager.GetRolesAsync(model).Result;
            string roleName = roles.FirstOrDefault();
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, model.UserName),
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role, roleName)
            };
             var token = new JwtSecurityToken(_configuration.GetSection("Jwt:Issuer").Value, _configuration.GetSection("Jwt:Audience").Value, claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: credentials);
              return new JwtSecurityTokenHandler().WriteToken(token);   
        }
    }
}
