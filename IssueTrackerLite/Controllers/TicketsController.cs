using AutoMapper;
using IssueTrackerLite.Data;
using IssueTrackerLite.DTOs;
using IssueTrackerLite.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IssueTrackerLite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController(ITicketRepository repository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAll()
        {
            var tickets = await repository.GetAllAsync();
            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetById(int id)
        {
            var ticket = await repository.GetByIdAsync(id);
            if (ticket == null) return NotFound();
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> Create(TicketDto ticketDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("No user ID was found in token.");
            }

            var ticket = mapper.Map<Ticket>(ticketDto);
            ticket.UserId = userId;

            var created = await repository.AddAsync(ticket);
            return CreatedAtAction(nameof(GetById), new { id = created.TicketID }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Ticket>> Update(int id, Ticket ticket)
        {
            if (id != ticket.TicketID)
            {
                return BadRequest();
            }

            var updated = await repository.UpdateAsync(ticket);

            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await repository.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
