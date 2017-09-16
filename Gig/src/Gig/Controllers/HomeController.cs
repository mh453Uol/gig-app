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
using Gig.Repositories;
using Gig.Persistence;

namespace Gig.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private UserManager<ApplicationUser> _userManager;

        public HomeController(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string q = null)
        {
            var model = new GigsViewModel()
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = q,
                UpcomingGigs = _unitOfWork.Gig.GetUpcomingGigs(q)
            };

            if (model.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);

                model.Attending = _unitOfWork.Attendance
                    .GetGigsUserAttending(model.UpcomingGigs, userId)
                    .ToLookup(a => a.GigId);
            }

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
