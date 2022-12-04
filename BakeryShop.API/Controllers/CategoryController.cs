using BakeryShop.BusinessObject.DTOs.Product;
using BakeryShop.BusinessObject.Response;
using BakeryShop.BusinessObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BakeryShop.BusinessObject.DTOs.Category;
using BakeryShop.Data.Repository.Impl;
using BakeryShop.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BakeryShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            var response = new BaseResponse<IEnumerable<Category>>
            {
                Result = null,
                Errors = null,
                StatusCode = 0
            };
            var transaction = await _categoryRepository.GetAllCategories();
            if (transaction.IsError)
            {
                response.Errors = response.Errors.Append(transaction.ErrorMessage);
                response.StatusCode = BadRequest().StatusCode;
                response.IsError = true;
                return BadRequest(response);
            }
            else
            {
                response.Result = transaction.Resposne;
                response.StatusCode = Ok().StatusCode;
                return Ok(response);
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery] string id)
        {
            var response = new BaseResponse<Category>
            {
                Result = null,
                StatusCode = 0
            };
            var transactionResult = _categoryRepository.GetById(new Guid(id));
            if (transactionResult.IsError)
            {
                response.Errors = response.Errors.Append(transactionResult.ErrorMessage);
                response.StatusCode = BadRequest().StatusCode;
                return BadRequest(response);
            }
            else
            {
                response.Result = transactionResult.Resposne;
                response.StatusCode = Ok().StatusCode;
                return Ok(response);
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CategoryDTO model)
        {
            var response = new BaseResponse<Category>
            {
                Errors = null,
                Result = null,
                StatusCode = 0
            };
            var category = _mapper.Map<Category>(model);
            if (category != null)
            {
                var transaction = await _categoryRepository.Create(category);
                if (transaction.ErrorMessage == null)
                {
                    response.Result = transaction.Resposne;
                    response.StatusCode = 201;
                    return Created("",response);
                }
                else
                {
                    response.Errors = response.Errors.Append(transaction.ErrorMessage);
                    return BadRequest(response);
                }
            }
            else
            {
                var errors = new List<string>();
                errors.Add("Invalid parameter");
                return BadRequest(new BaseResponse<string>
                {
                    Errors = errors
                });
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(string id, CategoryDTO model)
        {
            var categoryID = new Guid(id);
            var category = _mapper.Map<Category>(model);
            category.CategoryId = categoryID;
            var response = new BaseResponse<Category>
            {
                Errors = null,
                Result = null,
                StatusCode = Ok().StatusCode
            };
            var transactionResult = await _categoryRepository.Update(category);
            if (transactionResult.IsError)
            {
                response.Errors = response.Errors.Append(transactionResult.ErrorMessage);
                response.StatusCode = BadRequest().StatusCode;
                return BadRequest(response);
            }
            else
            {
                response.Result = transactionResult.Resposne;
                return Ok(response);
            }
        }
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = new BaseResponse<Category>
            {
                Result = null,
                Errors = new List<string>(),
                StatusCode = BadRequest().StatusCode
            };
            try
            {
                Guid categoryID = new Guid(id);
                await _categoryRepository.Delete(categoryID);
                response.StatusCode = Ok().StatusCode;
                return Ok(response);
            }
            catch(Exception ex)
            {
                response.IsError = true;
                response.Errors = response.Errors.Append(ex.Message);
                return BadRequest(response);
            }
        }
    }
}
