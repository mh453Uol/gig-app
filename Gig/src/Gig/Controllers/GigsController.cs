using Microsoft.AspNetCore.Mvc;
using Gig.Data;
using Gig.Models.GigsViewModels;
using Microsoft.AspNetCore.Authorization;
using Gig.Models;
using Microsoft.AspNetCore.Identity;
using Gig.Helper;
using System.Linq;
using System.Collections.Generic;

namespace Gig.Controllers
{
    public class GigsController : Controller
    {
        public readonly ApplicationDbContext db;
        private IUserService userService;

        public GigsController(ApplicationDbContext _db,
            UserManager<ApplicationUser> _userManager,
            IUserService _userService)
        {
            this.db = _db;
            this.userService = _userService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var gigs = db.Gigs.ToList();
            var model = AutoMapper.Mapper.Map<IEnumerable<Models.Gig>,
                IEnumerable<GigsFormViewModel>>(gigs);
            return View();
        }

        [Authorize]
        public IActionResult Create()
        {
            var model = new GigsFormViewModel()
            {
                Genres = db.Genres
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GigsFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = db.Genres.ToList();
                return View(model);
            }

            var gig = AutoMapper.Mapper.Map<GigsFormViewModel, Models.Gig>(model);
            gig.ArtistId = userService.GetUserId();
            db.Add(gig);
            db.SaveChanges();
            return RedirectToAction("Index", "Home", null);
        }

    }
}
