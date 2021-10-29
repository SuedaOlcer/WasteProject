using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WasteProject.Data;
using WasteProject.Data.Entities;
using WasteProject.Models;

namespace WasteProject.Controllers
{
    public class UserGiftsController : Controller
    {
        private readonly MainContext _context;

        public UserGiftsController(MainContext context)
        {
            _context = context;
        }

       
        public async Task<IActionResult> Index()
        {
            var mainContext = _context.UserGifts.Include(u => u.Gift).Include(u => u.User);
            return View(await mainContext.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGifts = await _context.UserGifts
                .Include(u => u.Gift)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userGifts == null)
            {
                return NotFound();
            }

            return View(userGifts);
        }


       
        public IActionResult Create()
        {
 
            ViewBag.Gifts = _context.Gifts.ToList().OrderBy(e => e.Point);
  
            var points = _context.UserPoints
                .Include(e=>e.User)
                .GroupBy(e => new { e.UserId,e.User.Name})
                .Select(e => new UserWithPoint { UserId=e.Key.UserId,Name=e.Key.Name, Point= e.Sum(e=>e.Point) }).ToList();
 

            ViewBag.UserPoint = points;

            return View();  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,GiftId")] UserGifts userGifts)
        {
            if (ModelState.IsValid)
            {
                var GiftPoint = _context.Gifts.FirstOrDefault(e => e.Id == userGifts.GiftId);

                var userPoint = _context.UserPoints
               .Where(e=>e.UserId==userGifts.UserId)
               .GroupBy(e => new { e.UserId })
               .Select(e => new UserWithPoint { 
                   UserId = e.Key.UserId, Point = e.Sum(e => e.Point) 
               }).FirstOrDefault();

                if (GiftPoint.Point > userPoint.Point)
                {
                    ModelState.AddModelError("GiftId", "Hediye Puanı Kullanıcı Puanından Yüksek Olamaz");
                }
                else
                {
                    UserPoints points = new()
                    {
                        UserId = userGifts.UserId,
                        Point = -GiftPoint.Point
                    };

                    _context.Add(userGifts);
                    _context.Add(points);

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }


            }
            ViewBag.Gifts = _context.Gifts.ToList().OrderBy(e => e.Point);

            ViewBag.UserPoint = _context.UserPoints
                .Include(e => e.User)
                .GroupBy(e => new { e.UserId, e.User.Name })
                .Select(e => new UserWithPoint { UserId = e.Key.UserId, Name = e.Key.Name, Point = e.Sum(e => e.Point) }).ToList();



            return View(userGifts);
        }

      

    }
}
