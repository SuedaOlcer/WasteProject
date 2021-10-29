using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WasteProject.Data;
using WasteProject.Data.Entities;

namespace WasteProject.Controllers
{
    public class WasteTypesController : Controller
    {
        private readonly MainContext _context;

        public WasteTypesController(MainContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.WasteTypes.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Point")] WasteTypes wasteTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wasteTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wasteTypes);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wasteTypes = await _context.WasteTypes.FindAsync(id);
            if (wasteTypes == null)
            {
                return NotFound();
            }
            return View(wasteTypes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Point")] WasteTypes wasteTypes)
        {
            if (id != wasteTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wasteTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WasteTypesExists(wasteTypes.Id))
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
            return View(wasteTypes);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wasteTypes = await _context.WasteTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wasteTypes == null)
            {
                return NotFound();
            }

            return View(wasteTypes);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wasteTypes = await _context.WasteTypes.FindAsync(id);
            _context.WasteTypes.Remove(wasteTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WasteTypesExists(int id)
        {
            return _context.WasteTypes.Any(e => e.Id == id);
        }
    }
}
