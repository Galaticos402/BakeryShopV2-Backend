using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BakeryShop.BusinessObject
{
    public class OrderDetail
    {
        [Key]
        public Guid OrderId { get; set; }
        [Key]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; } = null!;
        [JsonIgnore]
        public virtual Product Product { get; set; } = null!;
    }
}
