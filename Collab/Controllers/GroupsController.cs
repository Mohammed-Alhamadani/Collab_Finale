using Collab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Collab.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private readonly AppDbContext _context;

        public GroupsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Groups
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Groups;
            return View(await appDbContext.ToListAsync());
        }

        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.GroupID == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View(); // No need to pass anything to ViewData, because CreatorUserID and CreatedDate will be set automatically
        }

        // POST: Groups/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupName,Description")] Group @group)
        {
            // Automatically get the CreatorUserID from the current logged-in user's claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // This retrieves the logged-in user's ID
            if (userId == null)
            {
                return Unauthorized(); // If user ID is null, return unauthorized
            }

            // Attempt to parse the string userId to an integer
            int parsedUserId;
            if (int.TryParse(userId, out parsedUserId))
            {
                // Set the CreatorUserID and CreatedDate for the new group
                @group.CreatorUserID = parsedUserId;
                @group.CreatedDate = DateTime.Now;

                // No need to set @group.Creator here, as EF will handle it based on CreatorUserID

                // Save the new group to the database if the model is valid
                if (ModelState.IsValid)
                {
                    _context.Add(@group);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index)); // Redirect to the list after successful creation
                }
            }
            else
            {
                // If userId is not a valid integer, return Unauthorized
                return Unauthorized();
            }

            // If something went wrong, return to the view with the same group data
            return View(@group);
        }

        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups.FindAsync(id);
            if (group == null)
            {
                return NotFound();
            }

            // Ensure the logged-in user is the creator (optional security check)
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (group.CreatorUserID != int.Parse(userId))
            {
                return Unauthorized(); // Prevent editing groups that don't belong to the current user
            }

            return View(group);
        }

        // POST: Groups/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GroupID,GroupName,Description")] Group @group)
        {
            if (id != @group.GroupID)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            @group.CreatorUserID = int.Parse(userId);

            if (ModelState.IsValid)
            {
                try
                {
                    // Don't modify CreatedDate, leave it as is in the database
                    var existingGroup = await _context.Groups.AsNoTracking().FirstOrDefaultAsync(g => g.GroupID == id);
                    @group.CreatedDate = existingGroup?.CreatedDate ?? DateTime.Now; // Keep the original CreatedDate if it's not null, else set to current date

                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.GroupID))
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
            return View(@group);
        }

        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(m => m.GroupID == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @group = await _context.Groups.FindAsync(id);
            if (@group != null)
            {
                _context.Groups.Remove(@group);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.GroupID == id);
        }
    }
}
