using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BakeryShop.BusinessObject
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        public Guid CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }

        public string? ImageName { get; set; }

        public string? ThumbnailImage { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}