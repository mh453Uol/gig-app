using Microsoft.AspNetCore.Mvc;
using Gig.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Gig.Persistence;

namespace Gig.Controllers
{
    [Authorize]
    public class FollowingController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public FollowingController(UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork)
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