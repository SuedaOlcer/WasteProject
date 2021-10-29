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
    public class UserPointsController : Controller
    {
        private readonly MainContext _context;

        public UserPointsController(MainContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var points = _context.UserPoints
                .Include(e => e.User)
                .GroupBy(e => new { e.UserId, e.User.Name })
                .Select(e => new UserWithPoint { UserId = e.Key.UserId, Name = e.Key.Name, Point = e.Sum(e => e.Point) }).ToList();


            return View(points);
        }

      

    }
}
