using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OllsWarehouse.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OllsWarehouse.Controllers
{
    [Authorize(Roles = "Manager")]
    public class MovieManagementController : Controller
    {
        private readonly ILogger<MovieManagementController> _logger;
        private readonly ApplicationDbContext _context;

        public MovieManagementController(ILogger<MovieManagementController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Films> filmModel = _context.Films.ToList();
            return View(filmModel);
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(FilmManipulation filmModel)
        {
            if (ModelState.IsValid)
            {
                Films newMovie = new Films
                {
                    movieTitle = filmModel.movieTitle,
                    movieCertificate = filmModel.movieCertificate,
                    movieDescription = filmModel.movieDescription,
                    movieImage = filmModel.movieImage,
                    moviePrice = filmModel.moviePrice,
                    Stars = filmModel.Stars,
                    movieReleaseDate = DateTime.Now,
                    movieTrailer = filmModel.movieTrailer,
                    movieGenre = filmModel.movieGenre,
                    movieBackgroundImage = filmModel.movieBackgroundImage,
                };

                _context.Add(newMovie);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View();

        }

        [HttpGet]
        public IActionResult UpdateMovie(int id)
        {
            Films filmModel = _context.Films.Find(id);

            FilmManipulation filmManipModel = new FilmManipulation
            {
                movieID = filmModel.movieID,
                movieTitle = filmModel.movieTitle,
                movieCertificate = filmModel.movieCertificate,
                movieDescription = filmModel.movieDescription,
                movieImage = filmModel.movieImage,
                moviePrice = filmModel.moviePrice,
                Stars = filmModel.Stars,
                movieReleaseDate = DateTime.Now,
                movieTrailer = filmModel.movieTrailer,
                movieGenre = filmModel.movieGenre,
                movieBackgroundImage = filmModel.movieBackgroundImage,
            };

            ViewBag.movieImageName = filmModel.movieImage;
            return View(filmManipModel);

        }

        [HttpPost]
        public IActionResult UpdateMovie(FilmManipulation filmModel)
        {
            if (ModelState.IsValid)
            {
                Films filmManipModel = new Films
                {
                    movieID = filmModel.movieID,
                    movieTitle = filmModel.movieTitle,
                    movieCertificate = filmModel.movieCertificate,
                    movieDescription = filmModel.movieDescription,
                    movieImage = filmModel.movieImage,
                    moviePrice = filmModel.moviePrice,
                    Stars = filmModel.Stars,
                    movieReleaseDate = DateTime.Now,
                    movieTrailer = filmModel.movieTrailer,
                    movieGenre = filmModel.movieGenre,
                    movieBackgroundImage = filmModel.movieBackgroundImage,
                };

                _context.Films.Update(filmManipModel);

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(filmModel);

        }

        [HttpGet]
        public IActionResult DeleteMovie(int id)
        {
            Films filmModel = _context.Films.Find(id);
            return View(filmModel);
        }

        [HttpPost]
        public IActionResult DeleteMovie(Films filmModel)
        {
            _context.Films.Remove(filmModel);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
