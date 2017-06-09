using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Gig.Data;
using Gig.Models.GigsViewModels;

namespace Gig.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext db { get; set; }

        public GigsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Create()
        {
            var model = new GigsFormViewModel()
            {
                Genres = db.Genres
            };

            return View(model);
        }
    }
}
