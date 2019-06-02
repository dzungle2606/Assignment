using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelixAssignment.DAL;
using HelixAssignment.ViewModel;

namespace HelixAssignment.BAL
{
    public interface IHelixEventService
    {
        Task<ICollection<HelixEvent>> GetAllEvents();

        Task<HelixEvent> GetEventById(long id);

        Task<bool> Update(HelixEventViewModel requestItem);
    }
}
