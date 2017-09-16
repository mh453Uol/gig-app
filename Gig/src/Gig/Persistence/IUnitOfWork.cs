using System.Threading.Tasks;
using Gig.Repositories;

namespace Gig.Persistence
{
    public interface IUnitOfWork
    {
        IApplicationUserRepository ApplicationUser { get; }
        IAttendanceRepository Attendance { get; }
        IFollowingRepository Following { get; }
        IGenreRepository Genre { get; }
        IGigRepository Gig { get; }
        INotificationRepository Notification { get; }

        void Complete();
        Task<int> CompleteAsync();
    }
}