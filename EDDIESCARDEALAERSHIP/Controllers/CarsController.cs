using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EDDIESCARDEALAERSHIP.Data;
using EDDIESCARDEALAERSHIP.Models;
using Microsoft.AspNetCore.Identity;

namespace EDDIESCARDEALAERSHIP.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public CarsController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                ViewBag.IsAccess = _context.ApplicationUsers.Where(x => x.Id == userId).Select(x => x.isAccess).FirstOrDefault();
                if (ViewBag.IsAccess)
                {
                    return View(await _context.Cars.ToListAsync());
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                ViewBag.IsAccess = _context.ApplicationUsers.Where(x => x.Id == userId).Select(x => x.isAccess).FirstOrDefault();
                if (ViewBag.IsAccess)
                {
                    return View(car);
                }
            }
            return RedirectToAction("Index", "Home");

        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                ViewBag.IsAccess = _context.ApplicationUsers.Where(x => x.Id == userId).Select(x => x.isAccess).FirstOrDefault();
                if (ViewBag.IsAccess)
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");

        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Year,Make,Model,Trim,PurchaseDate,PurchasePrice,Repairs,RepairCost,LotDate,SellingPrice,SaleDate,IsAvailable,Photo")] Car car, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    // Define the target folder
                    var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

                    // Check if the folder exists, create it if not
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    // Get a unique filename to avoid overwriting existing files
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                    // Combine the path with the unique filename
                    var filePath = Path.Combine(uploadFolder, uniqueFileName);

                    // Save the file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    car.Photo = uniqueFileName;

                }
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);


        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                ViewBag.IsAccess = _context.ApplicationUsers.Where(x => x.Id == userId).Select(x => x.isAccess).FirstOrDefault();
                if (ViewBag.IsAccess)
                {
                    return View(car);
                }
            }
            return RedirectToAction("Index", "Home");

        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Year,Make,Model,Trim,PurchaseDate,PurchasePrice,Repairs,RepairCost,LotDate,SellingPrice,SaleDate,IsAvailable,Photo")] Car car, IFormFile? file)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null && file.Length > 0)
                    {
                        // Define the target folder
                        var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

                        // Check if the folder exists, create it if not
                        if (!Directory.Exists(uploadFolder))
                        {
                            Directory.CreateDirectory(uploadFolder);
                        }

                        // Get a unique filename to avoid overwriting existing files
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                        // Combine the path with the unique filename
                        var filePath = Path.Combine(uploadFolder, uniqueFileName);

                        // Save the file to the server
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        car.Photo = uniqueFileName;

                    }
                    if (string.IsNullOrEmpty(car.Photo))
                    {
                        car.Photo = _context.Cars.Where(x => x.Id == id).Select(x => x.Photo).FirstOrDefault();
                    }
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                ViewBag.IsAccess = _context.ApplicationUsers.Where(x => x.Id == userId).Select(x => x.isAccess).FirstOrDefault();
                if (ViewBag.IsAccess)
                {
                    return View(car);
                }
            }
            return RedirectToAction("Index", "Home");

        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
