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
    public class GiftsController : Controller
    {
        private readonly MainContext _context;

        public GiftsController(MainContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gifts.ToListAsync());
        }

      
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Point,Url")] Gifts gifts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gifts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gifts);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gifts = await _context.Gifts.FindAsync(id);
            if (gifts == null)
            {
                return NotFound();
            }
            return View(gifts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Point,Url")] Gifts gifts)
        {
            if (id != gifts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gifts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GiftsExists(gifts.Id))
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
            return View(gifts);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gifts = await _context.Gifts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gifts == null)
            {
                return NotFound();
            }

            return View(gifts);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gifts = await _context.Gifts.FindAsync(id);
            _context.Gifts.Remove(gifts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GiftsExists(int id)
        {
            return _context.Gifts.Any(e => e.Id == id);
        }
    }
}
