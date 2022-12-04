using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.BusinessObject.Response
{
    public class BaseResponse<T>
    {
        public T Result { get; set; }
        public int StatusCode { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public bool IsError { get; set; }  = false;

    }
}
