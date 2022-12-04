using BakeryShop.BusinessObject;
using BakeryShop.BusinessObject.DTOs.Category;
using BakeryShop.BusinessObject.TransactionResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Repository
{
    public interface ICategoryRepository
    {
        Task<TransactionResult<IEnumerable<Category>>> GetAllCategories();
        Task<TransactionResult<Category>> Create(Category category);
        TransactionResult<Category> GetById(Guid id);
        Task<TransactionResult<Category>> Update(Category category);
        Task Delete(Guid id);
    }
}
