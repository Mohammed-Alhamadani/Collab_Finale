using Collab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collab.ApiControllers
{
    [Route("api/[controller]")]


    [ApiController]
    public class EventsAPIController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EventsAPIController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/EventsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        // GET: api/EventsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null) return NotFound();
            return @event;
        }

        // POST: api/EventsAPI
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _context.Events.Add(@event);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEvent), new { id = @event.EventID }, @event);
        }

        // PUT: api/EventsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event @event)
        {
            if (id != @event.EventID) return BadRequest();
            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Events.Any(e => e.EventID == id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        // DELETE: api/EventsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null) return NotFound();
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
