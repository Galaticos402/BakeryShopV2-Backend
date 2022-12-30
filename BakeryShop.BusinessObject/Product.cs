using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BakeryShop.BusinessObject
{
    public class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        public Guid ProductId { get; set; }
        [Required]
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        [Required]
        public decimal OriginalPrice { get; set; }
        public decimal? DiscountedPrice { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = null!;

        public Product Clone()
        {
            var product = (Product)MemberwiseClone();
            return product;
        }
    }
}
