using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gig.Data;
using Gig.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Gig.Repositories;
using Gig.Persistence;

namespace Gig.Controllers
{
    [Authorize]
    public class FollowingController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly UnitOfWork _unitOfWork;

        public FollowingController(UserManager<ApplicationUser> userManager,
            UnitOfWork unitOfWork)
        {
            this._userManager = userManager;
            this._unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var following = _unitOfWork.Following.GetUserFollowees(userId);

            return View(following);
        }
    }
}