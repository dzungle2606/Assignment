using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HelixAssignment.DAL
{
    public interface IProductDbService
    {
        Task<Product> GetById(long id);

        Task<bool> Update(Product product);

        Task<bool> UpdateBatch(ICollection<Product> products);
    }
}
