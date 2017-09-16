using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gig.Data;
using Gig.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Gig.Persistence;

namespace Gig.WebApiControllers
{
    [Produces("application/json")]
    [Route("api/Gigs")]
    [Authorize]
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

        [Route("Cancel")]
        [HttpDelete]
        public async Task<IActionResult> Cancel(Guid gigId)
        {
            if (gigId == Guid.Empty) { return BadRequest(); }

            var userId = _userManager.GetUserId(HttpContext.User);

            var gig = await _unitOfWork.Gig.GetGigWithAttendees(gigId);

            if (gig == null) { return BadRequest(); }

            if (gig.ArtistId != userId) { return Unauthorized(); }

            if (gig.CantCancel())
            {
                return BadRequest("Gig is already cancelled or a past gig");
            }

            gig.Cancelled();
            await _unitOfWork.CompleteAsync();
            return Ok();
        }
    }
}