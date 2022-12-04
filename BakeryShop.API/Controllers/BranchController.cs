using AutoMapper;
using BakeryShop.BusinessObject;
using BakeryShop.BusinessObject.DTOs.Branch;
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
    public class BranchController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IBranchRepository _branchRepository;
        public BranchController(IMapper mapper, IBranchRepository branchRepository)
        {
            _mapper = mapper;
            _branchRepository = branchRepository;
        }
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            var response = new BaseResponse<IEnumerable<Branch>>
            {
                Result = null
            };
            try
            {
                var branches = await _branchRepository.GetAll();
                response.Result = branches;
                response.StatusCode = Ok().StatusCode;
                return Ok(response);
            }
            catch(Exception e)
            {
                response.IsError = true;
                response.Errors = response.Errors.Append(e.Message);
                response.StatusCode = BadRequest().StatusCode;
                return BadRequest(response);
            }
            
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] BranchDTO model)
        {
            var response = new BaseResponse<Branch>
            {
                Result = null
            };
            var branch = _mapper.Map<Branch>(model);
            try
            {
                await _branchRepository.Create(branch);
                response.Result = branch;
                response.StatusCode = Ok().StatusCode;
                return Ok(response);
            }catch(Exception e)
            {
                response.IsError = true;
                response.Errors = response.Errors.Append(e.Message);
                response.StatusCode = BadRequest().StatusCode;
                return BadRequest(response);
            }
        }
    }
}
