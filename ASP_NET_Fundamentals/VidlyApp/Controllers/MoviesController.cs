using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VidlyApp.Models;

namespace VidlyApp.Controllers
{
    public class MoviesController : Controller
    {

        private List<Movie> _movies = new List<Movie>
            {
                new Movie { Id = 1, Name = "The Shawshank Redemption" },
                new Movie { Id = 2, Name = "The Godfather" },
                new Movie { Id = 3, Name = "The Dark Knight" }
            };
        public ActionResult Index()
        {
            return View(_movies);
        }
    }
}