using Microsoft.AspNetCore.Mvc;
using Gig.Data;
using Gig.Helper;
using Microsoft.AspNetCore.Identity;
using Gig.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Gig.Controllers
{
    public class HomeController : Controller
    {
        public readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext _db)
        {
            this.db = _db;
        }

        public async Task<IActionResult> Index()
        {
            var upcomingGigs = await db.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateAndTime > DateTime.Now)
                .AsNoTracking()
                .ToListAsync();

            return View(upcomingGigs);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
