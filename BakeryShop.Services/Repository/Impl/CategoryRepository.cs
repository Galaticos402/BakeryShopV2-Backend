using AutoMapper;
using BakeryShop.BusinessObject;
using BakeryShop.BusinessObject.DTOs.Category;
using BakeryShop.BusinessObject.Response;
using BakeryShop.BusinessObject.TransactionResult;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryShop.Data.Repository.Impl
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly BakeryShopContext _bakeryShopContext;
        public CategoryRepository(BakeryShopContext bakeryShopContext)
        {
            _bakeryShopContext = bakeryShopContext;
        }
        public async Task<TransactionResult<Category>> Create(Category category)
        {
            try
            {
                await _bakeryShopContext.Categories.AddAsync(category);
                await _bakeryShopContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new TransactionResult<Category>
                {
                    Resposne = null,
                    ErrorMessage = ex.Message
                };
            }
            return new TransactionResult<Category>
            {
                Resposne = category,
                ErrorMessage = null,
                IsError = true
            };
        }

        public async Task Delete(Guid id)
        {
            var getCategoryTransaction = GetById(id);
            if (getCategoryTransaction.IsError)
            {
                throw new Exception(getCategoryTransaction.ErrorMessage);
            }
            else
            {
                var result = _bakeryShopContext.Categories.Remove(getCategoryTransaction.Resposne);
                await _bakeryShopContext.SaveChangesAsync();
            }
        }

        public async Task<TransactionResult<IEnumerable<Category>>> GetAllCategories()
        {
            try
            {
                var result = await _bakeryShopContext.Categories.ToListAsync();
                return new TransactionResult<IEnumerable<Category>>
                {
                    Resposne = result,
                    ErrorMessage = null
                };
            }catch(Exception e)
            {
                return new TransactionResult<IEnumerable<Category>>
                {
                    Resposne = null,
                    ErrorMessage = e.Message,
                    IsError = true
                };
            }
        }

        public TransactionResult<Category> GetById(Guid id)
        {
            var category = _bakeryShopContext.Categories.Where(c => c.CategoryId.Equals(id)).FirstOrDefault();
            if(category == null)
            {
                return new TransactionResult<Category>
                {
                    Resposne = category,
                    ErrorMessage = "Unable to find category with corresponding ID",
                    IsError = true
                };
            }
            return new TransactionResult<Category>
            {
                Resposne = category,
                ErrorMessage = null
            };
        }


        public async Task<TransactionResult<Category>> Update(Category category)
        {
            var response = new TransactionResult<Category>
            {
                Resposne = null,
                ErrorMessage = "",
            };
            var getCategoryIDTransaction = GetById(category.CategoryId);
            if (getCategoryIDTransaction.IsError)
            {
                // If fail to find the category, return that method's response
                return getCategoryIDTransaction;
            }
            else
            {
                _bakeryShopContext.Entry<Category>(getCategoryIDTransaction.Resposne).CurrentValues.SetValues(category);
                await _bakeryShopContext.SaveChangesAsync();
                return new TransactionResult<Category>
                {
                    Resposne = category,
                };
            }
        }

    }
}
