using Collab.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Collab.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public UsersController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager; // Injected UserManager
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Username,Email,ProfilePictureURL")] User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError("Password", "Password is required.");
                return View(user);
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email already exists.");
                return View(user);
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            user.DateJoined = DateTime.Now;
            user.Role = "User"; // Default role
            _context.Add(user);
            await _context.SaveChangesAsync();

            var userRole = await _userManager.FindByEmailAsync(user.Email);
            await _userManager.AddToRoleAsync(userRole, "User");

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                ModelState.AddModelError("Password", "Invalid login attempt.");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home"); // Redirect to home after successful login
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home"); // Redirect to home after logout
        }

        // (Other action methods like Details, AccessDenied, etc. can remain unchanged)
    }
}