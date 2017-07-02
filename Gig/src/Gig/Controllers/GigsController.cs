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
using System;

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

            var model = new GigsViewModel()
            {
                IsAuthenticated = true,
                UpcomingGigs = attending.Select(g => g.Gig),
                Heading = "Gigs I'm Attending"
            };

            return View("_Gigs", model);
        }

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GigsFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await _db.Genres.ToListAsync();
                return View(model);
            }
            var user = _userManager.GetUserId(User);
            var gig = AutoMapper.Mapper.Map<GigsFormViewModel, Models.Gig>(model);
            gig.ArtistId = user;

            _db.Add(gig);

            var followers = _db.Followers.Where(f => f.FolloweeId == user && !f.IsDeleted)
                .Select(f => f.Follower);

            var notification = new Notification(gig.Id, NotificationType.GigCreated);

            _db.Add(notification);

            foreach (var follower in followers)
            {
                follower.Notify(notification);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("Mine");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid gigId)
        {
            if (gigId == Guid.Empty) { return NotFound(); }

            var userId = _userManager.GetUserId(HttpContext.User);

            var gig = await _db.Gigs
                .AsNoTracking()
                .SingleAsync(g => g.ArtistId == userId && g.Id == gigId);

            var model = AutoMapper.Mapper.Map<Models.Gig, GigsFormViewModel>(gig);
            model.Genres = await _db.Genres.ToListAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(GigsFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await _db.Genres.ToListAsync();
                return View("Edit", model);
            }
            var userId = _userManager.GetUserId(HttpContext.User);

            var gig = await _db.Gigs
                .AsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == model.Id && g.ArtistId == userId);

            if (gig == null) { return NotFound(); }

            var updatedGig = AutoMapper.Mapper.Map<GigsFormViewModel, Models.Gig>(model);
            updatedGig.ArtistId = userId;

            _db.Update(updatedGig);
            await _db.SaveChangesAsync();
            return RedirectToAction("Mine");
        }

        public async Task<IActionResult> Mine()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var gigs = await _db.Gigs
                .Include(g => g.Genre)
                .Where(g => g.ArtistId == userId)
                .OrderBy(g => g.DateAndTime)
                .ToListAsync();

            var model = new MineGigViewModel(gigs);

            return View(model);
        }
    }
}
