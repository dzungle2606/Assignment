using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HelixAssignment.DAL
{
    public class HelixEventDbService : IHelixEventDbService
    {
        private readonly HelixDbContext _dbContext;

        public HelixEventDbService(HelixDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<HelixEvent>> GetAllEvents()
        {
            return await _dbContext.HelixEvents.Include(x => x.ProductsLink).ToListAsync();
        }

        public async Task<HelixEvent> GetById(long id)
        {
            HelixEvent helixEventItem = null;

            if (_dbContext.HelixEvents != null)
            { 
                if (_dbContext.HelixEvents.Count() != 0)
                {
                    helixEventItem = await _dbContext.HelixEvents.FindAsync(id);
                }
            }

            return helixEventItem;
        }

        public async Task<bool> Update(HelixEvent requestItem)
        {
            HelixEvent helixEventItem = await GetById(requestItem.HelixEventId);

            if (helixEventItem != null)
            {
                /// Update the existing event
                helixEventItem.Timestamp = DateTime.Now;
            }
            else
            {
                _dbContext.HelixEvents.Add(requestItem);
            }

            try
            {
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return true;

        }
    }
}
