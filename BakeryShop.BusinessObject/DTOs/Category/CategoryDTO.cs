using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.BusinessObject.DTOs.Category
{
    public class CategoryDTO
    {
        [Required]
        public string CategoryName { get; set; }

        public string? ImageName { get; set; }

        public string? ThumbnailImage { get; set; }
    }
}
