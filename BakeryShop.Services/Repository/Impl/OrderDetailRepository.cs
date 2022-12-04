using BakeryShop.BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Repository.Impl
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly BakeryShopContext _bakeryShopContext;
        public OrderDetailRepository(BakeryShopContext bakeryShopContext)
        {
            _bakeryShopContext = bakeryShopContext;
        }

        public async Task Create(IEnumerable<OrderDetail> model)
        {
            try
            {
                await _bakeryShopContext.OrderDetails.AddRangeAsync(model);
                await _bakeryShopContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<OrderDetail>> GetAll()
        {
            try
            {
                return await _bakeryShopContext.OrderDetails.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<OrderDetail>> GetByOrderID(Guid orderID)
        {
            try
            {
                var orderDetailsList = await _bakeryShopContext.OrderDetails.Where(x => x.OrderId.Equals(orderID)).ToListAsync();
                return orderDetailsList;
            }
            catch
            {
                throw;
            }
        }
    }
}
