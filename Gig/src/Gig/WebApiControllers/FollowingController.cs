using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gig.Dtos;
using Gig.Data;
using Microsoft.AspNetCore.Identity;
using Gig.Models;
using Microsoft.EntityFrameworkCore;

namespace Gig.WebApiControllers
{
    [Produces("application/json")]
    [Route("api/Following")]
    public class FollowingController : Controller
    {
        private readonly ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;

        public FollowingController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }

        [HttpPost]
        [Route("Follow")]
        public async Task<IActionResult> Follow(FollowingDto model)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var following = await _db.Followers
                .FirstAsync(f => f.FollowerId == userId &&
                f.FolloweeId == model.FolloweeId);

            if (following != null)
            {
                if (following.IsDeleted)
                {
                    following.IsDeleted = false;
                    _db.Update(following);

                    await _db.SaveChangesAsync();
                    return Ok();
                }

                return BadRequest("Your already following the artist");
            }

            _db.Add(new Following(userId, model.FolloweeId));
            await _db.SaveChangesAsync();
            return Ok();
        }

        [Route("Unfollow")]
        public async Task<IActionResult> Unfollow(FollowingDto model)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var following = await _db.Followers
                .SingleAsync(f => f.FollowerId == userId &&
                    f.FolloweeId == model.FolloweeId && f.IsDeleted == false);

            var errorMessage = @"You can't unfollow this user since your 
                not following them in the first place";

            if (following == null) { return BadRequest(errorMessage); }

            following.IsDeleted = true;
            _db.Update(following);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}