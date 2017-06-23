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

namespace Gig.WebApiControllers
{
    [Produces("application/json")]
    [Route("api/Gigs")]
    [Authorize]
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;

        public GigsController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }

        [Route("Cancel")]
        [HttpDelete]
        public async Task<IActionResult> Cancel(Guid gigId)
        {
            if(gigId == Guid.Empty) { return BadRequest();  }

            var userId = _userManager.GetUserId(HttpContext.User);

            var gig = _db.Gigs
                .SingleOrDefault(g => g.Id == gigId && g.ArtistId == userId);

            if(gig == null) { return BadRequest();  }

            if (gig.IsCancelled) { return BadRequest("Gig is already cancelled");  }

            gig.IsCancelled = true;

            _db.Update(gig);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}