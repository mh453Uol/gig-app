using System.Collections.Generic;
using Gig.Models;

namespace Gig.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string followerId, string followeeId, bool IsDeleted = false);
        IEnumerable<Following> GetUserFollowees(string userId);
        void Add(Following following);
    }
}