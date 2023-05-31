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
    public class ShippingTypeController : Controller
    {
        private readonly AppDbContext _context;

        public ShippingTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ShippingType
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShippingTypes.ToListAsync());
        }

        // GET: Admin/ShippingType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingType = await _context.ShippingTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shippingType == null)
            {
                return NotFound();
            }

            return View(shippingType);
        }

        // GET: Admin/ShippingType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ShippingType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] ShippingTypeDTO shippingType)
        {
            var shippingTypeDb = new ShippingType();
            
            if (ModelState.IsValid)
            {
                shippingTypeDb.Id = Guid.NewGuid();
                shippingTypeDb.Title = shippingType.Title;
                shippingTypeDb.ShippingInfos = shippingType.ShippingInfos;
                _context.Add(shippingTypeDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shippingTypeDb);
        }

        // GET: Admin/ShippingType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingType = await _context.ShippingTypes.FindAsync(id);
            if (shippingType == null)
            {
                return NotFound();
            }
            return View(shippingType);
        }

        // POST: Admin/ShippingType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] ShippingType shippingType)
        {
            if (id != shippingType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shippingType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShippingTypeExists(shippingType.Id))
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
            return View(shippingType);
        }

        // GET: Admin/ShippingType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shippingType = await _context.ShippingTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shippingType == null)
            {
                return NotFound();
            }

            return View(shippingType);
        }

        // POST: Admin/ShippingType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var shippingType = await _context.ShippingTypes.FindAsync(id);
            _context.ShippingTypes.Remove(shippingType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShippingTypeExists(Guid id)
        {
            return _context.ShippingTypes.Any(e => e.Id == id);
        }
    }
}
