using Microsoft.AspNetCore.Mvc;
using Gig.Data;
using Gig.Models.GigsViewModels;
using Microsoft.AspNetCore.Authorization;
using Gig.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Gig.Controllers
{
    public class GigsController : BaseController
    {
        public readonly ApplicationDbContext _db;

        public GigsController(ApplicationDbContext _db,
            UserManager<ApplicationUser> _userManager): base(_userManager)
        {
            this._db = _db;
        }

        [Authorize]
        public IActionResult Create()
        {
            var model = new GigsFormViewModel()
            {
                Genres = _db.Genres
            };

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async IActionResult Create(GigsFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var artist = await this.GetCurrentUserAsync();
                var gig = new Models.Gig
                {
                    GenreId = (byte)model.Genre,
                    Venue = model.Venue,
                    DateAndTime = DateTime.Parse(model.Date),
                    ArtistId = artist.

                }
            }

            model.Genres = db.Genres;
            var i = AutoMapper.Mapper.Map<Models.Gig>(model);
            return View();
        }

    }
}
