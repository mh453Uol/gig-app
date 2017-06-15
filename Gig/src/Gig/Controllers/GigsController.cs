using Microsoft.AspNetCore.Mvc;
using Gig.Data;
using Gig.Models.GigsViewModels;
using Microsoft.AspNetCore.Authorization;
using Gig.Models;
using Microsoft.AspNetCore.Identity;
using Gig.Helper;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Gig.Controllers
{
    public class GigsController : Controller
    {
        public readonly ApplicationDbContext db;
        private UserManager<ApplicationUser> _userManager;

        public GigsController(ApplicationDbContext _db,
            UserManager<ApplicationUser> _userManager)
        {
            this.db = _db;
            this._userManager = _userManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            //var gigs = await db.Gigs.ToListAsync();
            //var model = AutoMapper.Mapper.Map<IEnumerable<Models.Gig>,
            //    IEnumerable<GigsFormViewModel>>(gigs);
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var model = new GigsFormViewModel()
            {
                Genres = await db.Genres
                    .AsNoTracking()
                    .ToListAsync()
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GigsFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await db.Genres.ToListAsync();
                return View(model);
            }

            var gig = AutoMapper.Mapper.Map<GigsFormViewModel, Models.Gig>(model);
            gig.ArtistId = _userManager.GetUserId(User);
            db.Add(gig);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home", null);
        }

    }
}
