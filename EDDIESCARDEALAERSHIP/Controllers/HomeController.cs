using EDDIESCARDEALAERSHIP.Data;
using EDDIESCARDEALAERSHIP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;

namespace EDDIESCARDEALAERSHIP.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        //public HomeController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        //private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            Initialize();
        }

        public void Initialize()
        {
            if (_roleManager.FindByNameAsync("Admin").Result == null)
            {
                _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("User")).GetAwaiter().GetResult();
            }
            else { return; }

            ApplicationUser admin = new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "eddie@gmail.com",
                Email = "eddie@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "111111111111",
                isAccess = true,
            };

            _userManager.CreateAsync(admin, "eddie").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();

        }
        public IActionResult Index(decimal? minPrice, decimal? maxPrice)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                ViewBag.IsAccess = _context.ApplicationUsers.Where(x => x.Id == userId).Select(x => x.isAccess).FirstOrDefault();
            }
            else
            {
                ViewBag.IsAccess = false;
            }
            var cars = _context.Cars.ToList();

            if (minPrice.HasValue)
            {
                cars = cars.Where(c => c.SellingPrice >= minPrice.Value).ToList();
            }

            if (maxPrice.HasValue)
            {
                cars = cars.Where(c => c.SellingPrice <= maxPrice.Value).ToList();
            }

            return View(cars);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> UserList(string userid, bool isaccess)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                ViewBag.IsAccess = _context.ApplicationUsers.Where(x => x.Id == userId).Select(x => x.isAccess).FirstOrDefault();
                if (ViewBag.IsAccess)
                {
                    if (!string.IsNullOrEmpty(userid))
                    {
                        var user = _context.ApplicationUsers.Where(x => x.Id == userid).FirstOrDefault();
                        user.isAccess = isaccess;
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                    }
                    var userList = _context.ApplicationUsers.ToList();
                    return View(userList);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
