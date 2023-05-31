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

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductInPcController : Controller
    {
        private readonly AppDbContext _context;

        public ProductInPcController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ProductInPc
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ProductInPcs.Include(p => p.Pc).Include(p => p.Product);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/ProductInPc/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInPc = await _context.ProductInPcs
                .Include(p => p.Pc)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productInPc == null)
            {
                return NotFound();
            }

            return View(productInPc);
        }

        // GET: Admin/ProductInPc/Create
        public IActionResult Create()
        {
            ViewData["PcId"] = new SelectList(_context.Pcs, "Id", "Description");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description");
            return View();
        }

        // POST: Admin/ProductInPc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PcId,ProductId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] ProductInPc productInPc)
        {
            if (ModelState.IsValid)
            {
                productInPc.Id = Guid.NewGuid();
                _context.Add(productInPc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PcId"] = new SelectList(_context.Pcs, "Id", "Description", productInPc.PcId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productInPc.ProductId);
            return View(productInPc);
        }

        // GET: Admin/ProductInPc/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInPc = await _context.ProductInPcs.FindAsync(id);
            if (productInPc == null)
            {
                return NotFound();
            }
            ViewData["PcId"] = new SelectList(_context.Pcs, "Id", "Description", productInPc.PcId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productInPc.ProductId);
            return View(productInPc);
        }

        // POST: Admin/ProductInPc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PcId,ProductId,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] ProductInPc productInPc)
        {
            if (id != productInPc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productInPc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductInPcExists(productInPc.Id))
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
            ViewData["PcId"] = new SelectList(_context.Pcs, "Id", "Description", productInPc.PcId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productInPc.ProductId);
            return View(productInPc);
        }

        // GET: Admin/ProductInPc/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInPc = await _context.ProductInPcs
                .Include(p => p.Pc)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productInPc == null)
            {
                return NotFound();
            }

            return View(productInPc);
        }

        // POST: Admin/ProductInPc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productInPc = await _context.ProductInPcs.FindAsync(id);
            _context.ProductInPcs.Remove(productInPc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductInPcExists(Guid id)
        {
            return _context.ProductInPcs.Any(e => e.Id == id);
        }
    }
}
