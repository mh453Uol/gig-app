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
using Gig.Persistence;

namespace Gig.WebApiControllers
{
    [Produces("application/json")]
    [Route("api/Following")]
    [Authorize]
    public class FollowingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> _userManager;

        public FollowingController(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }

        [HttpPost]
        [Route("Follow")]
        public ActionResult Follow(FollowingDto model)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var following = _unitOfWork.Following.GetFollowing(userId, model.FolloweeId, true);

            if (following != null)
            {
                if (following.IsDeleted)
                {
                    following.IsDeleted = false;

                    _unitOfWork.Complete();
                    return Ok();
                }

                return BadRequest("Your already following the artist");
            }
            var follow = new Following(userId, model.FolloweeId);
            _unitOfWork.Following.Add(follow);
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpDelete]
        [Route("Unfollow")]
        public ActionResult Unfollow(string followeeId)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var following = _unitOfWork.Following.GetFollowing(userId, followeeId);

            var errorMessage = @"You can't unfollow this user since your 
                not following them in the first place";

            if (following == null) { return BadRequest(errorMessage); }

            if (following.IsDeleted) { return BadRequest(errorMessage); }

            following.Unfollow();

            _unitOfWork.Complete();

            return Ok();
        }
    }
}