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
        private UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }

        public ActionResult Index(string q = null)
        {
            var model = new GigsViewModel();

            model.IsAuthenticated = User.Identity.IsAuthenticated;
            model.Heading = "Upcoming Gigs";

            var upcomingGigs = _db.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateAndTime > DateTime.Now &&
                g.IsCancelled == false);

            if (!String.IsNullOrWhiteSpace(q))
            {
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.FullName.Contains(q) ||
                           g.Genre.Name.Contains(q) ||
                           g.Venue.Contains(q));

                model.SearchTerm = q;
            }

            model.UpcomingGigs = upcomingGigs.AsNoTracking().ToList(); ;

            return View("_Gigs", model);
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
