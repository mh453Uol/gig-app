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
    [Authorize]
    public class GigsController : Controller
    {
        public readonly ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;

        public GigsController(ApplicationDbContext _db,
            UserManager<ApplicationUser> _userManager)
        {
            this._db = _db;
            this._userManager = _userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Attending()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var attending = await _db.Attendances
                .Where(g => g.AttendeeId == userId)
                .Include(g => g.Gig)
                    .ThenInclude(g => g.Genre)
                .Include(g => g.Gig)
                    .ThenInclude(g => g.Artist)
                .AsNoTracking()
                .ToListAsync();

            var model = new GigsViewModel() {
                IsAuthenticated = true,
                UpcomingGigs = attending.Select(g => g.Gig).ToList()
            };

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var model = new GigsFormViewModel()
            {
                Genres = await _db.Genres
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
                model.Genres = await _db.Genres.ToListAsync();
                return View(model);
            }

            var gig = AutoMapper.Mapper.Map<GigsFormViewModel, Models.Gig>(model);
            gig.ArtistId = _userManager.GetUserId(User);

            _db.Add(gig);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home", null);
        }

    }
}
