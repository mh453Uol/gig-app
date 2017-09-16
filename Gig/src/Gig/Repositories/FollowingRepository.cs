using Gig.Data;
using Gig.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _db;

        public FollowingRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(Following following)
        {
            _db.Followers.Add(following);
        }

        public Following GetFollowing(string followerId, string followeeId, bool IsDeleted = false)
        {
            return _db.Followers.FirstOrDefault(f => f.FollowerId == followerId &&
                        f.FolloweeId == followeeId && f.IsDeleted == IsDeleted);
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
