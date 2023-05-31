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
    public class SpecificationTypeController : Controller
    {
        private readonly AppDbContext _context;

        public SpecificationTypeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/SpecificationType
        public async Task<IActionResult> Index()
        {
            return View(await _context.SpecificationTypes.ToListAsync());
        }

        // GET: Admin/SpecificationType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specificationType = await _context.SpecificationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specificationType == null)
            {
                return NotFound();
            }

            return View(specificationType);
        }

        // GET: Admin/SpecificationType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/SpecificationType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] SpecificationTypeDTO specificationType)
        {
            var specificationTypeDb = new SpecificationType();
            
            if (ModelState.IsValid)
            {
                specificationTypeDb.Id = Guid.NewGuid();
                specificationTypeDb.Specifications = specificationType.Specifications;
                specificationTypeDb.Title = specificationType.Title;
                _context.Add(specificationTypeDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specificationTypeDb);
        }

        // GET: Admin/SpecificationType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specificationType = await _context.SpecificationTypes.FindAsync(id);
            if (specificationType == null)
            {
                return NotFound();
            }
            return View(specificationType);
        }

        // POST: Admin/SpecificationType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] SpecificationType specificationType)
        {
            if (id != specificationType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specificationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecificationTypeExists(specificationType.Id))
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
            return View(specificationType);
        }

        // GET: Admin/SpecificationType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specificationType = await _context.SpecificationTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specificationType == null)
            {
                return NotFound();
            }

            return View(specificationType);
        }

        // POST: Admin/SpecificationType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var specificationType = await _context.SpecificationTypes.FindAsync(id);
            _context.SpecificationTypes.Remove(specificationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecificationTypeExists(Guid id)
        {
            return _context.SpecificationTypes.Any(e => e.Id == id);
        }
    }
}
