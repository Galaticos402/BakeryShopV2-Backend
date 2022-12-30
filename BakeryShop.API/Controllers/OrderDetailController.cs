using AutoMapper;
using BakeryShop.BusinessObject;
using BakeryShop.BusinessObject.DTOs.OrderDetail;
using BakeryShop.BusinessObject.Response;
using BakeryShop.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BakeryShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderDetailController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderDetailRepository _orderDetailRepository;
        public OrderDetailController(IMapper mapper, IOrderDetailRepository orderRepository)
        {
            _mapper = mapper;
            _orderDetailRepository = orderRepository;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] IEnumerable<OrderDetailDTO> models)
        {
            var orderDetails = _mapper.Map<IEnumerable<OrderDetail>>(models);
            var resposne = new BaseResponse<IEnumerable<OrderDetail>>
            {
                Result = null
            };
            try
            {
                await _orderDetailRepository.Create(orderDetails);
                resposne.Result = orderDetails;
                resposne.StatusCode = 201;
                return Created("", resposne);
            }catch(Exception e)
            {
                resposne.IsError = true;
                resposne.Errors = resposne.Errors.Append(e.Message);
                resposne.StatusCode = BadRequest().StatusCode;
                return BadRequest(resposne);
            }
        }
    }
}
