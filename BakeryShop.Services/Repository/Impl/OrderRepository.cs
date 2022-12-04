using BakeryShop.BusinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Repository.Impl
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BakeryShopContext _bakeryShopContext;
        public OrderRepository(BakeryShopContext bakeryShopContext)
        {
            _bakeryShopContext = bakeryShopContext;
        }
        public async Task<Order> Create(Order model)
        {
            try
            {
                await _bakeryShopContext.Orders.AddAsync(model);
                await _bakeryShopContext.SaveChangesAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _bakeryShopContext.Orders.ToListAsync();
        }
    }
}
