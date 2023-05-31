#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Contracts.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain;
using Base.DAL.EF;
using WebApp.DTO;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PcController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PcController(IAppUnitOfWork uow)
        {
            _uow = uow;
            // ########################################################################
        }

        // GET: Admin/Pc
        public async Task<IActionResult> Index()
        {
            // ########################################################################
            var res = await _uow.Pcs.GetAllAsync();
            
            return View(res);
        }

        // GET: Admin/Pc/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            // ########################################################################
            var pc = await _uow.Pcs.FirstOrDefaultAsync(id.Value);
            
            if (pc == null)
            {
                return NotFound();
            }

            return View(pc);
        }

        // GET: Admin/Pc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Pc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Price,Description,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] App.DAL.DTO.Pc pc)
        {
            var pcDb = new Pc();
            
            if (ModelState.IsValid)
            {
                pc.Id = Guid.NewGuid();
                pc.Description = pc.Description;
                pc.Price = pc.Price;
                pc.Title = pc.Title;
                _uow.Pcs.Add(pc);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pc);
        }

        // GET: Admin/Pc/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pc = await _uow.Pcs.FirstOrDefaultAsync(id.Value);
            if (pc == null)
            {
                return NotFound();
            }
            return View(pc);
        }

        // POST: Admin/Pc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Title,Price,Description,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Id")] App.DAL.DTO.Pc pc)
        {
            if (id != pc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Pcs.Update(pc);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PcExists(pc.Id))
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
            return View(pc);
        }

        // GET: Admin/Pc/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pc = await _uow.Pcs
                .FirstOrDefaultAsync(id.Value);
            if (pc == null)
            {
                return NotFound();
            }

            return View(pc);
        }

        // POST: Admin/Pc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Pcs.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PcExists(Guid id)
        {
            return await _uow.Pcs.ExistsAsync(id);
        }
    }
}
