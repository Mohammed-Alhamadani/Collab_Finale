using Collab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Collab.Controllers
{
    public class YouTubeVideosController : Controller
    {
        private readonly AppDbContext _context;

        public YouTubeVideosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: YouTubeVideos
        public async Task<IActionResult> Index()
        {
            return View(await _context.YouTubeVideos.ToListAsync());
        }

        // GET: YouTubeVideos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var youTubeVideo = await _context.YouTubeVideos
                .FirstOrDefaultAsync(m => m.VideoID == id);
            if (youTubeVideo == null)
            {
                return NotFound();
            }

            return View(youTubeVideo);
        }

        // GET: YouTubeVideos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: YouTubeVideos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VideoID,Title,Description,VideoURL,CategoryID,UploadedByUserID")] YouTubeVideo youTubeVideo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(youTubeVideo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(youTubeVideo);
        }

        // GET: YouTubeVideos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var youTubeVideo = await _context.YouTubeVideos.FindAsync(id);
            if (youTubeVideo == null)
            {
                return NotFound();
            }
            return View(youTubeVideo);
        }

        // POST: YouTubeVideos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VideoID,Title,Description,VideoURL,CategoryID,UploadedByUserID")] YouTubeVideo youTubeVideo)
        {
            if (id != youTubeVideo.VideoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(youTubeVideo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YouTubeVideoExists(youTubeVideo.VideoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(youTubeVideo);
        }

        // GET: YouTubeVideos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var youTubeVideo = await _context.YouTubeVideos
                .FirstOrDefaultAsync(m => m.VideoID == id);
            if (youTubeVideo == null)
            {
                return NotFound();
            }

            return View(youTubeVideo);
        }

        // POST: YouTubeVideos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var youTubeVideo = await _context.YouTubeVideos.FindAsync(id);
            if (youTubeVideo != null)
            {
                _context.YouTubeVideos.Remove(youTubeVideo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YouTubeVideoExists(int id)
        {
            return _context.YouTubeVideos.Any(e => e.VideoID == id);
        }
    }
}
