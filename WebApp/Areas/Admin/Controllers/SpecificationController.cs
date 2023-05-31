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
    public class SpecificationController : Controller
    {
        private readonly AppDbContext _context;

        public SpecificationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Specification
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Specifications.Include(s => s.SpecificationType);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Specification/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specification = await _context.Specifications
                // .Include(s => s.Product)
                .Include(s => s.SpecificationType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specification == null)
            {
                return NotFound();
            }

            return View(specification);
        }

        // GET: Admin/Specification/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Title");
            ViewData["SpecificationTypeId"] = new SelectList(_context.SpecificationTypes, "Id", "Id");
            return View();
        }

        // POST: Admin/Specification/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,SpecificationTypeId,Description,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] SpecificationDTO specification)
        {
            var specificationDb = new Specification();
            
            if (ModelState.IsValid)
            {
                specificationDb.Id = Guid.NewGuid();
                specificationDb.Description = specification.Description;
                specificationDb.SpecificationType = specification.SpecificationType;
                specificationDb.SpecificationTypeId = specification.SpecificationTypeId;
                _context.Add(specificationDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Title", specification.ProductId);
            ViewData["SpecificationTypeId"] = new SelectList(_context.SpecificationTypes, "Id", "Id", specification.SpecificationTypeId);
            return View(specificationDb);
        }

        // GET: Admin/Specification/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specification = await _context.Specifications.FindAsync(id);
            if (specification == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Title");
            ViewData["SpecificationTypeId"] = new SelectList(_context.SpecificationTypes, "Id", "Id", specification.SpecificationTypeId);
            return View(specification);
        }

        // POST: Admin/Specification/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProductId,SpecificationTypeId,Description,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] Specification specification)
        {
            if (id != specification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecificationExists(specification.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Title");
            ViewData["SpecificationTypeId"] = new SelectList(_context.SpecificationTypes, "Id", "Id", specification.SpecificationTypeId);
            return View(specification);
        }

        // GET: Admin/Specification/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specification = await _context.Specifications
                .Include(s => s.SpecificationType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specification == null)
            {
                return NotFound();
            }

            return View(specification);
        }

        // POST: Admin/Specification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var specification = await _context.Specifications.FindAsync(id);
            _context.Specifications.Remove(specification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecificationExists(Guid id)
        {
            return _context.Specifications.Any(e => e.Id == id);
        }
    }
}
