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

namespace Gig.Controllers
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

        public async Task<IActionResult> Follow(FollowDto model)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var isFollowing = _db.Followers
                .Any(f => f.FollowerId == userId &&
                f.FolloweeId == model.FolloweeId);

            if (isFollowing)
            {
                return BadRequest("Your already following the artist");
            }

            var following = new Following(userId, model.FolloweeId);
            _db.Add(following);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}