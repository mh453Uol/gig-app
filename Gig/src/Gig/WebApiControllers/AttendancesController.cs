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

            var attendanceExist = _db.Attendances
                .SingleOrDefault(a => a.GigId == model.GigId &&
                    a.AttendeeId == userId && a.IsCancelled == true);

            if (attendanceExist == null)
            {
                var attendance = new Attendance(userId, model.GigId);
                _db.Attendances.Add(attendance);
            }
            else
            {
                attendanceExist.Attend();
            }

            _db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult Cancel(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = _userManager.GetUserId(User);

            var attendance = _db.Attendances
                .FirstOrDefault(a => a.GigId == id &&
                    a.AttendeeId == userId && a.IsCancelled == false);

            if (attendance == null)
            {
                return NotFound();
            }

            attendance.Cancel();

            _db.SaveChanges();

            return Ok(id);
        }
    }
}