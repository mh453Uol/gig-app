using Microsoft.AspNetCore.Mvc;
using Gig.Data;
using Gig.Models.GigsViewModels;
using Microsoft.AspNetCore.Authorization;
using Gig.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using Gig.Helper.User;
using Gig.Helper;

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
        public IActionResult Create(GigsFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var gig = AutoMapper.Mapper.Map<GigsFormViewModel,Models.Gig>(model);
                gig.DateAndTime = DateTime.Parse(model.Date).Add(TimeSpan.Parse(model.Time));
                gig.ArtistId = userService.GetUserId();
                db.Add(gig);
                db.SaveChanges();

                return RedirectToAction("Index","Home",null);
            }
            model.Genres = db.Genres;

            return View(model);
        }

    }
}
