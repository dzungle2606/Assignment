using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HelixAssignment.DAL
{
    public class HelixEventProductDbService : IHelixEventProductDbService
    {
        private readonly HelixDbContext _dbContext;

        public HelixEventProductDbService(HelixDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<HelixEventProduct> GetAllByEventId(long eventId)
        {
            List<HelixEventProduct> list = null;

            try
            {
                list = _dbContext.HelixEventProducts.Where(e => e.EventId == eventId).ToList<HelixEventProduct>();
            }
            catch (Exception ex)
            {
                /** In real application, 
                 * Encapsulate the details in the layer-specific log files for further troubleshooting 
                 * and wrap it in user-friendly manner
                 **/
                throw ex;
            }


            return list;
        }

        public bool DeleteByEventId(long eventId)
        {
            ICollection<HelixEventProduct> removedList = GetAllByEventId(eventId);

            if (removedList != null && removedList.Count() > 0 )
            { 
                try
                {
                    _dbContext.HelixEventProducts.RemoveRange(removedList);
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    /** In real application, 
                   * Encapsulate the details in the layer-specific log files for further troubleshooting 
                   * and wrap it in user-friendly manner
                   **/
                   throw ex;
                }
            }

            return true;
        }

        public async Task<bool> UpdateBatch(long eventId, ICollection<HelixEventProduct> eventProducts)
        {
            bool isDeleted = false;
            try
            {
                isDeleted = DeleteByEventId(eventId);
            }
            catch (Exception ex)
            {
                /** In real application, 
                * Encapsulate the details in the layer-specific log files for further troubleshooting 
                * and wrap it in user-friendly manner
                **/
                throw ex;
            }           

            if (isDeleted)
            {
                try
                {
                    foreach (HelixEventProduct eventProduct in eventProducts)
                    {
                        await _dbContext.HelixEventProducts.AddAsync(eventProduct);
                    }

                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    /// Logging details into DB or S3
                    /// Which eventId, etc
                    /// Encapsulate the exception details etc
                    throw ex;
                }
            }
            else
            {
                /** In real application, 
                * Encapsulate the details in the layer-specific log files for further troubleshooting 
                * and wrap it in user-friendly manner
                **/
                throw new Exception("Could not delete the previously updated products for the event!!!");
            }

            return true;
        }

    }
}
