using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}