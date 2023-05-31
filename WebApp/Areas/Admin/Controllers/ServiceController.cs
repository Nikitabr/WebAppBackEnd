#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.DAL.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using WebApp.DTO;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Service
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Services.Include(s => s.AppUser).Include(s => s.ServiceType);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Service/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.AppUser)
                .Include(s => s.ServiceType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Admin/Service/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "Id", "Id");
            return View();
        }

        // POST: Admin/Service/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,ServiceTypeId,Description,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] ServiceDTO service)
        {
            var serviceDb = new Service();
            
            if (ModelState.IsValid)
            {
                serviceDb.Id = Guid.NewGuid();
                serviceDb.Description = service.Description;
                serviceDb.Orders = service.Orders;
                serviceDb.AppUser = service.AppUser;
                serviceDb.ServiceType = service.ServiceType;
                serviceDb.AppUserId = service.AppUserId;
                serviceDb.ServiceTypeId = service.ServiceTypeId;
                _context.Add(serviceDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", service.AppUserId);
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "Id", "Id", service.ServiceTypeId);
            return View(serviceDb);
        }

        // GET: Admin/Service/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", service.AppUserId);
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "Id", "Id", service.ServiceTypeId);
            return View(service);
        }

        // POST: Admin/Service/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,ServiceTypeId,Description,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", service.AppUserId);
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "Id", "Id", service.ServiceTypeId);
            return View(service);
        }

        // GET: Admin/Service/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.AppUser)
                .Include(s => s.ServiceType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Admin/Service/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var service = await _context.Services.FindAsync(id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(Guid id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
