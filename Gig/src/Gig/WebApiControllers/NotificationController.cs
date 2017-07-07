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

namespace Gig.WebApiControllers
{
    [Produces("application/json")]
    [Route("api/Notification")]
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

        [HttpPost]
        [Route("seen")]
        public async Task<IActionResult> Seen(List<int> notifications)
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            var unReadNotification = _db.UserNotification.Where(n =>
                notifications.Contains(n.NotificationId) && n.UserId == userId)
                .ToList();

            unReadNotification.ForEach(x => x.IsRead = true);

            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}