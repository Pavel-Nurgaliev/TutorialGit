using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models.Movies;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {

        private readonly List<Movie> _moviesList;

        public MoviesController()
        {
            _moviesList = new List<Movie>()
            {
                new Movie() {Id = 1, Name = "Shrek" },
                new Movie() {Id = 2, Name = "Wall-e" }
            };
        }

        public ActionResult Index()
        {
            var viewModel = new MoviesViewModel()
            {
                Movies = _moviesList
            };

            return View(viewModel);
        }

        public ActionResult Details(int id)
        {
            var movie = _moviesList.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }
    }
}