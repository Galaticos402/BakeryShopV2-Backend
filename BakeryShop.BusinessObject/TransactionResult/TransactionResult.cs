using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.BusinessObject.TransactionResult
{
    public class TransactionResult<T>
    {
        public T Resposne { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsError { get; set; } = false;
    }
}
