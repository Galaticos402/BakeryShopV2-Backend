using BakeryShop.BusinessObject;
using BakeryShop.BusinessObject.TransactionResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetByID(Guid id);
        Task<TransactionResult<Product>> Create(Product product);

        Task<TransactionResult<IEnumerable<Product>>> GetByCategoryID(Guid categoryID);
        Task<Product> Update(Product product);
        Task Delete(Guid id);
    }
}
