using Collab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Collab.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YouTubeVideosAPIController : ControllerBase
    {
        private readonly AppDbContext _context;

        public YouTubeVideosAPIController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/YouTubeVideosAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<YouTubeVideo>>> GetYouTubeVideos()
        {
            return await _context.YouTubeVideos.ToListAsync();
        }

        // GET: api/YouTubeVideosAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<YouTubeVideo>> GetYouTubeVideo(int id)
        {
            var youTubeVideo = await _context.YouTubeVideos.FindAsync(id);
            if (youTubeVideo == null) return NotFound();
            return youTubeVideo;
        }

        // POST: api/YouTubeVideosAPI
        [HttpPost]
        public async Task<ActionResult<YouTubeVideo>> PostYouTubeVideo(YouTubeVideo youTubeVideo)
        {
            _context.YouTubeVideos.Add(youTubeVideo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetYouTubeVideo), new { id = youTubeVideo.VideoID }, youTubeVideo);
        }

        // PUT: api/YouTubeVideosAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutYouTubeVideo(int id, YouTubeVideo youTubeVideo)
        {
            if (id != youTubeVideo.VideoID) return BadRequest();
            _context.Entry(youTubeVideo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.YouTubeVideos.Any(e => e.VideoID == id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        // DELETE: api/YouTubeVideosAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteYouTubeVideo(int id)
        {
            var youTubeVideo = await _context.YouTubeVideos.FindAsync(id);
            if (youTubeVideo == null) return NotFound();
            _context.YouTubeVideos.Remove(youTubeVideo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
