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

            var notifications = await _db.UserNotification
                .Include(n => n.Notification)
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            return View("Notification", notifications);
        }
    }
}
