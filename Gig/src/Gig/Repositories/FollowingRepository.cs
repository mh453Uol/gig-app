using Gig.Data;
using Gig.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Repositories
{
    public class FollowingRepository
    {
        private readonly ApplicationDbContext _db;

        public FollowingRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Following> GetFollowing(string followerId, string followeeId)
        {
            return _db.Followers.Where(f => f.FollowerId == followerId &&
                        f.FolloweeId == followeeId && f.IsDeleted == false)
                    .AsNoTracking()
                    .ToList();
        }

        public IEnumerable<Following> GetUserFollowees(string userId)
        {
            return _db.Followers
                .Include(f => f.Followee)
                .Where(f => f.FollowerId == userId && f.IsDeleted == false)
                .AsNoTracking()
                .ToList();

        }
    }
}
