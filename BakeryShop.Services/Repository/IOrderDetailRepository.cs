using BakeryShop.BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Repository
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetail>> GetAll();
        Task<IEnumerable<OrderDetail>> GetByOrderID(Guid orderID);
        Task Create(IEnumerable<OrderDetail> model);
    }
}
