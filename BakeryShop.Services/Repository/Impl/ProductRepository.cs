using BakeryShop.BusinessObject;
using BakeryShop.BusinessObject.TransactionResult;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Repository.Impl
{
    public class ProductRepository : IProductRepository
    {
        private readonly BakeryShopContext _bakeryShopContext;
        public ProductRepository(BakeryShopContext bakeryShopContext)
        {
            _bakeryShopContext = bakeryShopContext;
        }

        public async Task<TransactionResult<Product>> Create(Product product)
        {
            try
            {
                await _bakeryShopContext.Products.AddAsync(product);
                await _bakeryShopContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return new TransactionResult<Product>
            {
                Resposne = product,
                ErrorMessage = null
            };
        }

        public async Task Delete(Guid id)
        {
            try
            {
                var product = await GetByID(id);
                _bakeryShopContext.Products.Remove(product);
                await _bakeryShopContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public  async Task<IEnumerable<Product>> GetAll()
        {
            return await _bakeryShopContext.Products.ToListAsync();
        }

        public async Task<TransactionResult<IEnumerable<Product>>> GetByCategoryID(Guid categoryID)
        {
            var products = await _bakeryShopContext.Products.Where(x => x.CategoryId.Equals(categoryID)).ToListAsync();
            return new TransactionResult<IEnumerable<Product>>
            {
                Resposne = products
            };
        }

        public async Task<Product> GetByID(Guid id)
        {
            var product = await _bakeryShopContext.Products.Where(x => x.ProductId.Equals(id)).FirstOrDefaultAsync();
            if (product == null) throw new Exception("Unable to find product");
            return product;
        }

        public async Task<Product> Update(Product model)
        {
            try
            {
                var product = await GetByID(model.ProductId);
                _bakeryShopContext.Entry<Product>(product).CurrentValues.SetValues(model);
                await _bakeryShopContext.SaveChangesAsync();
                return model;
            }catch(Exception e)
            {
                throw;
            }
        }
    }
}
