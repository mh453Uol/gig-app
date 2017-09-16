using Gig.Models;

namespace Gig.Repositories
{
    public interface IApplicationUserRepository
    {
        ApplicationUser GetUserFollowers(string userId);
    }
}