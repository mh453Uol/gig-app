using Microsoft.AspNetCore.Mvc;
using Gig.Data;
using Gig.Helper;
using Microsoft.AspNetCore.Identity;
using Gig.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;
using Gig.Models.GigsViewModels;

namespace Gig.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task<IActionResult> Index()
        {
            var model = new GigsViewModel();

            model.IsAuthenticated = User.Identity.IsAuthenticated;
            model.Heading = "Upcoming Gigs";

            var upcomingGigs = await _db.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateAndTime > DateTime.Now)
                .AsNoTracking()
                .ToListAsync();

            model.UpcomingGigs = upcomingGigs;

            return View("_Gigs",model);
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
