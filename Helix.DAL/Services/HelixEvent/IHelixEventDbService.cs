using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HelixAssignment.DAL
{
    public interface IHelixEventDbService
    {
        Task<ICollection<HelixEvent>> GetAllEvents();

        Task<HelixEvent> GetById(long id);

        Task<bool> Update(HelixEvent requestItem);

    }
}
