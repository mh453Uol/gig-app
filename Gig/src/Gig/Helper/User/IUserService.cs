using Gig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Helper
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUser();
        string GetUserId();
    }
}
