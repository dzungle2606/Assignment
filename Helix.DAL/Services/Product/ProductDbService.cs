using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HelixAssignment.DAL
{
    public class ProductDbService : IProductDbService
    {
        private readonly HelixDbContext _dbContext;

        public ProductDbService(HelixDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> GetById(long id)
        {
            Product productItem = null;

            if (_dbContext.Products != null)
            { 
                if (_dbContext.Products.Count() != 0)
                {
                    productItem = await _dbContext.Products.FindAsync(id);
                }
            }

            return productItem;
        }

        public async Task<bool> Update(Product product)
        {
            Product productItem = await GetById(product.ProductId);

            if (productItem != null)
            {
                productItem.Name = product.Name;
            }
            else
            {
                _dbContext.Add(product);
            }

            return true;
        }

        public async Task<bool> UpdateBatch(ICollection<Product> products)
        {
            try
            {
                foreach (Product product in products)
                {
                    await Update(product);
                }

                try
                {
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

           

            return true;
        }
    }
}
