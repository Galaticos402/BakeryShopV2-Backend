using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.BusinessObject.DTOs.Product
{
    public class ProductDTO
    {
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public decimal OriginalPrice { get; set; }
        public decimal? DiscountedPrice { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}
