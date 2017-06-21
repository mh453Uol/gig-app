using Gig.Data;
using Gig.Dtos;
using Gig.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.WebApiControllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Attendances")]
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;

        public AttendancesController(ApplicationDbContext _db,
            UserManager<ApplicationUser> _userManager)
        {
            this._db = _db;
            this._userManager = _userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Attend(AttendanceDto model)
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var gig = await _db.Gigs
                .AsNoTracking()
                .FirstAsync(g => g.Id == model.GigId);

            if(gig == null) { return BadRequest();  }

            var isAttending = await _db.Attendances
                .AsNoTracking()
                .AnyAsync(a => a.AttendeeId == userId && a.GigId == model.GigId);

            if (isAttending) { return BadRequest("Your already attending this Gig");  }

            var attendance = new Attendance(userId, model.GigId);
            _db.Add(attendance);
            await _db.SaveChangesAsync();

            return Ok(attendance);
        }
    }
}