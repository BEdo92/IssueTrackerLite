using IssueTrackerLite.Data;
using IssueTrackerLite.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IssueTrackerLite.Repositories
{
    public class TicketRepository(DataContext context) : ITicketRepository
    {
        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await context.Tickets.ToListAsync();
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
            return await context.Tickets.FindAsync(id);
        }

        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            context.Tickets.Add(ticket);
            await context.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket?> UpdateAsync(Ticket ticket)
        {
            var existing = await context.Tickets.FindAsync(ticket.TicketID);
            if (existing == null) return null;

            context.Entry(existing).CurrentValues.SetValues(ticket);
            await context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ticket = await context.Tickets.FindAsync(id);
            if (ticket == null) return false;

            context.Tickets.Remove(ticket);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
