using Collab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collab.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YouTubeCategoriesAPIController : ControllerBase
    {
        private readonly AppDbContext _context;

        public YouTubeCategoriesAPIController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/YouTubeCategoriesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<YouTubeCategory>>> GetYouTubeCategories()
        {
            return await _context.YouTubeCategories.ToListAsync();
        }

        // GET: api/YouTubeCategoriesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<YouTubeCategory>> GetYouTubeCategory(int id)
        {
            var youTubeCategory = await _context.YouTubeCategories.FindAsync(id);
            if (youTubeCategory == null) return NotFound();
            return youTubeCategory;
        }

        // POST: api/YouTubeCategoriesAPI
        [HttpPost]
        public async Task<ActionResult<YouTubeCategory>> PostYouTubeCategory(YouTubeCategory youTubeCategory)
        {
            _context.YouTubeCategories.Add(youTubeCategory);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetYouTubeCategory), new { id = youTubeCategory.CategoryID }, youTubeCategory);
        }

        // PUT: api/YouTubeCategoriesAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYouTubeCategory(int id, YouTubeCategory youTubeCategory)
        {
            if (id != youTubeCategory.CategoryID) return BadRequest();
            _context.Entry(youTubeCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.YouTubeCategories.Any(e => e.CategoryID == id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        // DELETE: api/YouTubeCategoriesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYouTubeCategory(int id)
        {
            var youTubeCategory = await _context.YouTubeCategories.FindAsync(id);
            if (youTubeCategory == null) return NotFound();
            _context.YouTubeCategories.Remove(youTubeCategory);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
