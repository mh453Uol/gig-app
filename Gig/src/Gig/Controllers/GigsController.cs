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
using AutoMapper;
using Gig.Repositories;
using Gig.Persistence;

namespace Gig.Controllers
{
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> _userManager;

        public GigsController(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Attending()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            List<Attendance> attending = await _unitOfWork.Attendance.GetGigsUserAttending(userId);

            var model = new GigsViewModel()
            {
                IsAuthenticated = true,
                UpcomingGigs = attending.Select(g => g.Gig),
                Attending = attending.ToLookup(a => a.GigId),
                Heading = "Gigs I'm Attending"
            };

            return View("_Gigs", model);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var model = new GigsFormViewModel()
            {
                Genres = await _unitOfWork.Genre.GetAllGenres()
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
                model.Genres = await _unitOfWork.Genre.GetAllGenres();
                return View(model);
            }

            var userId = _userManager.GetUserId(User);

            var user = _unitOfWork.ApplicationUser.GetUserFollowers(userId);

            var gig = AutoMapper.Mapper.Map<GigsFormViewModel, Models.Gig>(model);

            gig.Artist = user;

            gig.Created();

            _unitOfWork.Gig.Add(gig);

            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Mine");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid gigId)
        {
            var gig = await _unitOfWork.Gig.GetGig(gigId);

            if (gig == null) { return NotFound(); }

            if (gig.ArtistId != _userManager.GetUserId(HttpContext.User)) { return Unauthorized(); }

            var model = AutoMapper.Mapper.Map<Models.Gig, GigsFormViewModel>(gig);

            model.Genres = await _unitOfWork.Genre.GetAllGenres();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(GigsFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await _unitOfWork.Genre.GetAllGenres();

                return View("Edit", model);
            }

            var gig = await _unitOfWork.Gig.GetGigWithAttendees(model.Id);

            if (gig == null) { return NotFound(); }

            if (gig.ArtistId != _userManager.GetUserId(HttpContext.User)) { return Unauthorized(); }

            if (gig.CantCancel())
            {
                return BadRequest("Gig is cancelled or a past gig so you cant update");
            }

            gig.Updated(gig.DateAndTime, gig.Venue);

            Mapper.Map<GigsFormViewModel, Models.Gig>(model, gig);

            await _unitOfWork.CompleteAsync();

            return RedirectToAction("Mine");
        }

        [Authorize]
        public async Task<IActionResult> Mine()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var gigs = await _unitOfWork.Gig.GetArtistGigsWithGenres(userId);

            var model = new MineGigViewModel(gigs);

            return View(model);
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel model)
        {
            return RedirectToAction("Index", "Home", new { q = model.SearchTerm });
        }

        [HttpGet]
        public ActionResult Detail(Guid id)
        {
            if (Guid.Empty == id)
            {
                return NotFound();
            }

            var gig = _unitOfWork.Gig.GetGigWithArtist(id);

            if (gig == null) { NotFound(); }


            var model = new GigDetailViewModel()
            {
                Gig = gig
            };

            var userId = _userManager.GetUserId(User);

            if (userId != null)
            {
                model.IsAttending = _unitOfWork.Attendance
                    .GetAttendance(userId, gig.Id) != null;

                model.IsFollowingArtist = _unitOfWork.Following
                    .GetFollowing(userId, gig.ArtistId) != null;

                model.IsAuthenticated = true;
            }

            return View(model);
        }
    }
}
