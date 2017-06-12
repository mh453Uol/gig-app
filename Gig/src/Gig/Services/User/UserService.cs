using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gig.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Gig.Helper.User
{
    public class UserService : IUserService
    {
        public IHttpContextAccessor context { get; set; }
        public UserManager<ApplicationUser> userManager { get; set; }
        public UserService(IHttpContextAccessor _contextAccessor, UserManager<ApplicationUser> _userManager)
        {
            this.context = _contextAccessor;
            this.userManager = _userManager;
        }
        public async Task<ApplicationUser> GetUser()
        {
            var user = context.HttpContext.User;
            return await userManager.GetUserAsync(user);
        }

        public string GetUserId()
        {
            var user = context.HttpContext.User;
            return userManager.GetUserId(user);
        }


    }
}
