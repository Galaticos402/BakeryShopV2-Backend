using BakeryShop.BusinessObject.DTOs.Product;
using BakeryShop.BusinessObject.DTOs.User;
using BakeryShop.BusinessObject.Response;
using BakeryShop.Data.Repository;
using BakeryShop.Data.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace BakeryShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        public UserController(IUserRepository userRepository, IUserService userService)
        {
            _userRepository = userRepository;
            _userService = userService;
        }
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            var result = await _userRepository.Create(model);
            if(result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login (UserLoginModel model)
        {
            var authenticatedUser = await _userService.Authenticate(model);
            if(authenticatedUser != null)
            {
                var token = await _userService.GenerateToken(authenticatedUser);
                return Ok(new BaseResponse<string>
                {
                    Result = token,
                    StatusCode = Ok().StatusCode
                });
            }
            return BadRequest("User not found");
        }
    }
}
