using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.BusinessObject
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        public Guid OrderId { get; set; }
        [Required]
        public Guid UserID { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public string DeliveredAddress { get; set; } = null!;
        public string? DeliveredDate { get; set; }
        public string? DeliveredTime { get; set; }

        public bool IsPaid { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public Guid BranchId { get; set; }
        public bool IsTakenByCustomer { get; set; }
        public bool IsRefunded { get; set; }
        public virtual Branch Branch { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = null!;
    }
}
