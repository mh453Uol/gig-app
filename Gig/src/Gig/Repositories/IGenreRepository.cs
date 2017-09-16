using System.Collections.Generic;
using System.Threading.Tasks;
using Gig.Models;

namespace Gig.Repositories
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllGenres();
    }
}