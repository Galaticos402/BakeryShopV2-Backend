using AutoMapper;
using BakeryShop.BusinessObject;
using BakeryShop.BusinessObject.DTOs.Order;
using BakeryShop.BusinessObject.Response;
using BakeryShop.Data.Repository;
using BakeryShop.Data.Repository.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BakeryShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        public OrderController(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderRepository.GetAll();
            var response = new BaseResponse<IEnumerable<Order>>
            {
                Result = orders
            };
            return Ok(response);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(OrderDTO model)
        {
            var order = _mapper.Map<Order>(model);
            var response = new BaseResponse<Order>
            {
                Result = null
            };
            try
            {
                await _orderRepository.Create(order);
                response.Result = order;
                response.StatusCode = 201;
                return Created("",response);
            }catch(Exception e)
            {
                response.StatusCode = BadRequest().StatusCode;
                response.IsError = true;
                response.Errors = response.Errors.Append(e.Message);
                return BadRequest(response);
            }
        }
    }
}
