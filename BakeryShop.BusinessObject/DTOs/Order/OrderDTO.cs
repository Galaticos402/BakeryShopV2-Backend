using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.BusinessObject.DTOs.Order
{
    public class OrderDTO
    {
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
    }
}
