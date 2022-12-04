using BakeryShop.BusinessObject;
using BakeryShop.BusinessObject.DTOs.Branch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Repository.Impl
{
    public class BranchRepository : IBranchRepository
    {
        private readonly BakeryShopContext _bakeryShopContext;
        public BranchRepository(BakeryShopContext bakeryShopContext)
        {
            _bakeryShopContext = bakeryShopContext;
        }

        public async Task<Branch> Create(Branch model)
        {
            try
            {
                await _bakeryShopContext.Branches.AddAsync(model);
                await _bakeryShopContext.SaveChangesAsync();
                return model;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Branch>> GetAll()
        {
            return await _bakeryShopContext.Branches.ToListAsync();
        }
    }
}
