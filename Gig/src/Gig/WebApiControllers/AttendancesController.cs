using Gig.Data;
using Gig.Dtos;
using Gig.Models;
using Gig.Persistence;
using Gig.Repositories;
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
        private readonly IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> _userManager;

        public AttendancesController(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> _userManager)
        {
            this._unitOfWork = unitOfWork;
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

            var attendance = _unitOfWork.Attendance.GetAttendance(userId, model.GigId, true);

            if (attendance == null)
            {
                _unitOfWork.Attendance.Add(new Attendance(userId, model.GigId));
            }
            else
            {
                attendance.Attend();
            }

            _unitOfWork.Complete();

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

            var attendance = _unitOfWork.Attendance.GetAttendance(userId, id);

            if (attendance == null)
            {
                return NotFound();
            }

            attendance.Cancel();

            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}