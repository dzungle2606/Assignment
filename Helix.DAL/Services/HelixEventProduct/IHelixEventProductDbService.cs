using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HelixAssignment.DAL
{
    public interface IHelixEventProductDbService
    {
        ICollection<HelixEventProduct> GetAllByEventId(long eventId);

        bool DeleteByEventId(long eventId);

        Task<bool> UpdateBatch(long eventId, ICollection<HelixEventProduct> eventProducts);
    }
}
