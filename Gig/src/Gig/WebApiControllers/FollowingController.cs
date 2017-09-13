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
using Microsoft.AspNetCore.Authorization;

namespace Gig.WebApiControllers
{
    [Produces("application/json")]
    [Route("api/Following")]
    [Authorize]
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
                .FirstOrDefaultAsync(f => f.FollowerId == userId &&
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
            var follow = new Following(userId, model.FolloweeId);
            _db.Add(follow);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("Unfollow")]
        public async Task<IActionResult> Unfollow(string followeeId)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var following = await _db.Followers
                .SingleOrDefaultAsync(f => f.FollowerId == userId &&
                    f.FolloweeId == followeeId && f.IsDeleted == false);

            var errorMessage = @"You can't unfollow this user since your 
                not following them in the first place";

            if (following == null) { return BadRequest(errorMessage); }

            following.Unfollow();

            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}