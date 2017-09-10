using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Gig.Data;
using Gig.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Gig.Dtos;

namespace Gig.WebApiControllers
{

    [Authorize]
    [Route("api/Notification")]
    [Produces("application/json")]
    public class NotificationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;

        public NotificationController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }

        [HttpGet]
        [Route("GetNewNotifications")]
        public ActionResult GetNewNotifications()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var notifications = _db.UserNotification
                .Where(u => u.UserId == userId && !u.IsRead)
                .Include(u => u.Notification)
                    .ThenInclude(u => u.Gig.Artist)
                .AsNoTracking()
                .ToList();

            var notificationsForUser = notifications.Select(n => n.Notification)
                .ToList();

            return Ok(AutoMapper.Mapper.Map<IEnumerable<NotificationDto>>
                (notificationsForUser));
        }

        [HttpPost]
        [Route("Seen")]
        public async Task<IActionResult> Seen()
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            var unReadNotification = _db.UserNotification
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToList();

            unReadNotification.ForEach(x => x.Read());

            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}