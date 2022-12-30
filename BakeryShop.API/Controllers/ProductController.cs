using AutoMapper;
using Azure.Storage.Blobs;
using BakeryShop.BusinessObject;
using BakeryShop.BusinessObject.DTOs.Product;
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IConfiguration _configuration;
        private readonly IFileService _fileService;
        public ProductController(IProductRepository productRepository, IMapper mapper, IFileService fileService, IConfiguration configuration, BlobServiceClient blobServiceClient)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _fileService = fileService;
            _configuration = configuration;
            _blobServiceClient = blobServiceClient;
        }
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAll();
            var response = new BaseResponse<IEnumerable<Product>>
            {
                Result = products
            };
            return Ok(response);
        }
        [HttpGet]
        [Route("getByCategory")]
        public async Task<IActionResult> GetByCategory(Guid id)
        {
            var response = new BaseResponse<IEnumerable<Product>>
            {
                Result = null
            };
            var transactionResult = await _productRepository.GetByCategoryID(id);
            if (transactionResult.IsError)
            {
                response.StatusCode = BadRequest().StatusCode;
                return BadRequest(response);
            }
            else
            {
                response.StatusCode = Ok().StatusCode;
                var products = transactionResult.Resposne;
                var formattedProducts = new List<Product>();
                foreach(var p in products)
                {
                    var url = _fileService.GetFileUri(p.ImageUrl);
                    var clonedProduct = p.Clone();
                    clonedProduct.ImageUrl = url;
                    formattedProducts.Add(clonedProduct);
                    Console.WriteLine(p.ImageUrl);
                }
                response.Result = formattedProducts;

                return Ok(response);
            }
        }
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            var response = new BaseResponse<Product>
            {
                Result = null
            };
            try
            {
                var product = await _productRepository.GetByID(id);
                response.Result = product;
                response.StatusCode = Ok().StatusCode;
                return Ok(response);
            }catch(Exception e)
            {
                response.IsError = true;
                response.StatusCode = BadRequest().StatusCode;
                response.Errors = response.Errors.Append(e.Message);
                return BadRequest(response);
            }
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(ProductDTO model)
        {
            var response = new BaseResponse<Product>
            {
                Errors = null,
                Result = null,
                StatusCode = Ok().StatusCode
            };
            var product = _mapper.Map<Product>(model);
            if(product != null)
            {
                try
                {
                    var transaction = await _productRepository.Create(product);
                    response.Result = transaction.Resposne;
                    return Ok(response);
                }catch(Exception e)
                {
                    response.Errors = response.Errors.Append(e.Message);
                    response.IsError = true;
                    return BadRequest(response);
                }
            }
            else
            {
                var errors = new List<string>();
                errors.Add("Invalid request");
                return BadRequest(new BaseResponse<string>
                {
                    Errors = errors
                }) ;
            }
        }

        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductDTO model)
        {
            var response = new BaseResponse<Product>();
            var product = _mapper.Map<Product>(model);
            product.ProductId = id;
            try
            {
                var result = await _productRepository.Update(product);
                response.Result = result;
                response.StatusCode = Ok().StatusCode;
                return Ok(response);
            }catch(Exception e)
            {
                response.Errors = response.Errors.Append(e.Message);
                response.StatusCode = BadRequest().StatusCode;
                return BadRequest(response);
            }
        }
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var response = new BaseResponse<Product>
            {
                Result = null
            };
            try
            {
                await _productRepository.Delete(id);
                response.StatusCode = Ok().StatusCode;
                return Ok(response);
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
