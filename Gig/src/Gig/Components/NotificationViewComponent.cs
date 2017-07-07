using Gig.Data;
using Gig.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Components
{
    public class NotificationViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        private UserManager<ApplicationUser> _userManager;

        public NotificationViewComponent(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            this._db = db;
            this._userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var isUnreadNotificaiton = true;

            var notification = await _db.UserNotification
                .Include(n => n.Notification)
                    .ThenInclude(n => n.Gig)
                    .ThenInclude(n => n.Artist)
                .Where(n => n.UserId == userId && !n.IsRead)
                .AsNoTracking()
                .ToListAsync();

            if (notification.Count() == 0)
            {
                notification = await _db.UserNotification
                    .Include(n => n.Notification)
                        .ThenInclude(n => n.Gig)
                        .ThenInclude(n => n.Artist)
                    .Take(5)
                    .OrderByDescending(n => n.Notification.DateTime)
                    .AsNoTracking()
                    .ToListAsync();

                isUnreadNotificaiton = false;
            }

            ViewBag.IsUnreadNotification = isUnreadNotificaiton;
            return View("Notification", notification);
        }
    }
}
