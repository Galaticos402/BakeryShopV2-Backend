using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.BusinessObject.DTOs.Branch
{
    public class BranchDTO
    {
        public string BranchName { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        public int Coordinates { get; set; }
    }
}
