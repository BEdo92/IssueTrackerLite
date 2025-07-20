using System.Collections.Generic;
using System.Threading.Tasks;
using IssueTrackerLite.Data;

namespace IssueTrackerLite.Interfaces
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task<Ticket?> GetByIdAsync(int id);
        Task<Ticket> AddAsync(Ticket ticket);
        Task<Ticket?> UpdateAsync(Ticket ticket);
        Task<bool> DeleteAsync(int id);
    }
}
