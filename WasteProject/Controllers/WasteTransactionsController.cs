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
    public class WasteTransactionsController : Controller
    {
        private readonly MainContext _context;

        public WasteTransactionsController(MainContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.WasteTransaction
                .Include(e=>e.User)
                .Include(e=>e.Waste).ToListAsync());
        }
        public IActionResult Create()
        {
            ViewBag.Waste = _context.WasteTypes.ToList();
            return View();
        }
 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,WasteId,WasteAmount,Point")] WasteTransaction wasteTransaction)
        {
            if (ModelState.IsValid)
            {
                UserPoints userPoints = new()
                {
                    UserId = wasteTransaction.UserId,
                    Point  =wasteTransaction.Point
                };

                _context.Add(wasteTransaction);
                _context.Add(userPoints);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wasteTransaction);
        }

        private bool WasteTransactionExists(int id)
        {
            return _context.WasteTransaction.Any(e => e.Id == id);
        }
    }
}
