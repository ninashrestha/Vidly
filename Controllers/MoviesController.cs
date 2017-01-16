using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;
using Vidly.Migrations;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
{
        private ApplicationDbContext _context;
            public MoviesController()
        {
            _context = new ApplicationDbContext();
        }


        protected override void Dispose(bool disposing)
         {
             _context.Dispose();
       }

        public ActionResult New()
        {
            var genre = _context.Genre.ToList();
            var viewMovie = new NewMovie
            {
                Genre = genre
            };
            return View("new",viewMovie);
            }

        [HttpPost]
        public ActionResult Create(Movies Movie)
        {
           

            if (Movie.Id == 0)
            {
                Movie.DateAdded = DateTime.Now;
                _context.Movies.Add(Movie);
            }
            else
            {
                var movieIndb = _context.Movies.Single(c => c.Id == Movie.Id);
                movieIndb.Name = Movie.Name;
                movieIndb.genre = Movie.genre;
                movieIndb.GenreId = Movie.GenreId;
                movieIndb.NumberInStock = Movie.NumberInStock;
                movieIndb.ReleaseDate = Movie.ReleaseDate;

            }
            try { _context.SaveChanges(); }
            catch (DbEntityValidationException e) { Console.WriteLine(e); }
            
            return RedirectToAction("Index", "Movies");
        }

    // GET: Movies
    public ActionResult Index()
        {
            var movies = _context.Movies.Include(m => m.genre).ToList();
            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.genre).SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }
        public ActionResult Edit (int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();
            var viewModel = new NewMovie
            {
                Movie = movie,
                Genre = _context.Genre.ToList()

            };

            return View("New", viewModel);
        }

    }
}