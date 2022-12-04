using BakeryShop.BusinessObject;
using BakeryShop.BusinessObject.DTOs.Branch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Repository
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Branch>> GetAll();
        Task<Branch> Create(Branch model);
    }
}
