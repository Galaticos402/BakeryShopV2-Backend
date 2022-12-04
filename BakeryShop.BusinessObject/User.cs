using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.BusinessObject
{
    public class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }
        [Key]
        public Guid UserID { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
