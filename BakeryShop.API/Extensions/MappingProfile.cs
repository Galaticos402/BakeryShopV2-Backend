using AutoMapper;
using BakeryShop.BusinessObject;
using BakeryShop.BusinessObject.DTOs.Branch;
using BakeryShop.BusinessObject.DTOs.Category;
using BakeryShop.BusinessObject.DTOs.Order;
using BakeryShop.BusinessObject.DTOs.OrderDetail;
using BakeryShop.BusinessObject.DTOs.Product;

namespace BakeryShop.API.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapper for product
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
            // Mapper for category
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
            // Mapper for Branch
            CreateMap<BranchDTO, Branch>();
            CreateMap<Branch, BranchDTO>();
            // Mapper for Order
            CreateMap<OrderDTO, Order>();
            CreateMap<Order, OrderDTO>();
            // Mapper for Order Detail
            CreateMap<OrderDetail, OrderDetailDTO>();
            CreateMap<OrderDetailDTO, OrderDetail>();
        }
    }
}
