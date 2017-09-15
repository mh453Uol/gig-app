using Gig.Data;
using Gig.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Repositories
{
    public class ApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public ApplicationUser GetUserFollowers(string userId)
        {
            return _db.Users
                .Include(u => u.Followees)
                    .ThenInclude(u => u.Follower)
                .FirstOrDefault(u => u.Id == userId);
        }
    }
}
