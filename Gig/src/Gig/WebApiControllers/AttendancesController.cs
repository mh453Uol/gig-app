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
        public IActionResult Attend(AttendanceDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(HttpContext.User);

            var attendance = _db.Attendances
                .FirstOrDefault(a => a.AttendeeId == userId &&
                    a.GigId == model.GigId);

            if (attendance == null)
            {
                // if no attendances then add.
                attendance = new Attendance(userId, model.GigId);
                _db.Attendances.Add(attendance);
            }
            else
            {
                attendance.Toggle();
            }

            _db.SaveChanges();

            return Ok(attendance);
        }
    }
}