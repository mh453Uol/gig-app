using Gig.Data;
using Gig.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Repositories
{
    public class GenreRepository
    {
        private readonly ApplicationDbContext _db;

        public GenreRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await _db.Genres.AsNoTracking().ToListAsync();
        }
    }
}
