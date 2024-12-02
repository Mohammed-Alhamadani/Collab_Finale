using Collab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Collab.Controllers
{
    public class YouTubeCategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public YouTubeCategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: YouTubeCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.YouTubeCategories.ToListAsync());
        }

        // GET: YouTubeCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var youTubeCategory = await _context.YouTubeCategories
                .FirstOrDefaultAsync(m => m.CategoryID == id);
            if (youTubeCategory == null)
            {
                return NotFound();
            }

            return View(youTubeCategory);
        }

        // GET: YouTubeCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: YouTubeCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryID,CategoryName,Description")] YouTubeCategory youTubeCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(youTubeCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(youTubeCategory);
        }

        // GET: YouTubeCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var youTubeCategory = await _context.YouTubeCategories.FindAsync(id);
            if (youTubeCategory == null)
            {
                return NotFound();
            }
            return View(youTubeCategory);
        }

        // POST: YouTubeCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryID,CategoryName,Description")] YouTubeCategory youTubeCategory)
        {
            if (id != youTubeCategory.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(youTubeCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YouTubeCategoryExists(youTubeCategory.CategoryID))
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
            return View(youTubeCategory);
        }

        // GET: YouTubeCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var youTubeCategory = await _context.YouTubeCategories
                .FirstOrDefaultAsync(m => m.CategoryID == id);
            if (youTubeCategory == null)
            {
                return NotFound();
            }

            return View(youTubeCategory);
        }

        // POST: YouTubeCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var youTubeCategory = await _context.YouTubeCategories.FindAsync(id);
            if (youTubeCategory != null)
            {
                _context.YouTubeCategories.Remove(youTubeCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YouTubeCategoryExists(int id)
        {
            return _context.YouTubeCategories.Any(e => e.CategoryID == id);
        }
    }
}
