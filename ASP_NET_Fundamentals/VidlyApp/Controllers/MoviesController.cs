using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using VidlyApp.Models;

namespace VidlyApp.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };

            //return View(movie);
            //return Content("Hello world");
            //return HttpNotFound();
            //return RedirectToAction("Index", "Home", new {page = 1, sortBy = "name"});
            return new EmptyResult();
        }

        public ActionResult Edit(int movieId)
        {
            return Content("movieId=" + movieId);
        }

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (pageIndex.HasValue)
            {
                pageIndex = 1;
            }

            if (string.IsNullOrEmpty(sortBy))
            {
                sortBy = "Name";
            }

            return Content($"PageIndex = {pageIndex}, SortBy = {sortBy}");
        }

        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content($"Year = {year}, Month = {month}");
        }
    }
}