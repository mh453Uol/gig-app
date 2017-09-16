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
using Gig.Repositories;
using Gig.Persistence;

namespace Gig.WebApiControllers
{

    [Authorize]
    [Route("api/Notification")]
    [Produces("application/json")]
    public class NotificationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> _userManager;

        public NotificationController(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
        }

        [HttpGet]
        [Route("GetNewNotifications")]
        public ActionResult GetNewNotifications()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var notifications = _unitOfWork.Notification
                    .GetUnreadNotificationsWithArtist(userId)
                    .Select(n => n.Notification)
                    .ToList();

            return Ok(AutoMapper.Mapper.Map<IEnumerable<NotificationDto>>
                (notifications));
        }

        [HttpPost]
        [Route("Seen")]
        public ActionResult Seen()
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            var unread = _unitOfWork.Notification.GetUnreadNotifications(userId);

            unread.ForEach(x => x.Read());

            _unitOfWork.Complete();

            return Ok();
        }
    }
}