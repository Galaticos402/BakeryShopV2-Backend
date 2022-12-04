using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.BusinessObject
{
    public class Branch
    {
        public Branch()
        {
            Orders = new HashSet<Order>();
        }
        [Key]
        public Guid BranchId { get; set; }
        [Required]
        public string BranchName { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        public int Coordinates { get; set; }
        public virtual ICollection<Order> Orders { get; set; } = null!;
    }
}
