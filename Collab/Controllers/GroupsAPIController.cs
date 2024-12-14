using Collab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collab.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsAPIController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GroupsAPIController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/GroupsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        // GET: api/GroupsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) return NotFound();
            return group;
        }

        // POST: api/GroupsAPI
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetGroup), new { id = group.GroupID }, group);
        }

        // PUT: api/GroupsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(int id, Group group)
        {
            if (id != group.GroupID) return BadRequest();
            _context.Entry(group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Groups.Any(e => e.GroupID == id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        // DELETE: api/GroupsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) return NotFound();
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
