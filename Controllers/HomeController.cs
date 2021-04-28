using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using OllsWarehouse.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OllsWarehouse.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        const string userName = "_Name";
        const string userAge = "_Age";
        const string userCart = "_Cart";

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewFilms()
        {
            List<Films> filmModel = _context.Films.ToList();
            return View(filmModel);
        }

        [HttpGet]
        public IActionResult ViewFilmDetails(int id)
        {
            Films filmModel = _context.Films.Find(id);
            return View(filmModel);
        }

        [HttpPost]
        public IActionResult ViewFilmDetails(IFormCollection movieForm)
        {
            int movieID = int.Parse(movieForm["movieID"]);
            string movieImage = movieForm["movieImage"].ToString();
            string movieTitle = movieForm["movieTitle"].ToString();
            decimal moviePrice = decimal.Parse(movieForm["moviePrice"]);
            int movieAmount = int.Parse(movieForm["movieAmount"]);
            MovieShoppingCart movie = new MovieShoppingCart();
            movie.movieID = movieID;
            movie.movieImage = movieImage;
            movie.movieTitle = movieTitle;
            movie.moviePrice = moviePrice;
            movie.movieAmount = movieAmount;
            movie.orderDateAndTime = DateTime.Now;

            var shoppingCart = new List<MovieShoppingCart>();

            if (HttpContext.Session.GetString(userCart) != null)
            {
                string JSONserialized = HttpContext.Session.GetString(userCart);
                shoppingCart = JsonSerializer.Deserialize<List<MovieShoppingCart>>(JSONserialized);
                var newMovie = shoppingCart.FirstOrDefault(newItem => newItem.movieID == movieID);

                if (newMovie != null)
                {
                    newMovie.movieAmount += movieAmount;
                }
                else
                {
                    shoppingCart.Add(movie);
                }
            }
            else
            {
                shoppingCart.Add(movie);
            }

            HttpContext.Session.SetString(userCart, JsonSerializer.Serialize(shoppingCart));

            return RedirectToAction("ViewFilmDetails");
        }

        public IActionResult Search(string userInput, string movieCert, string movieGenre)
        {

            ViewBag.userName = HttpContext.Session.GetString(userName);
            ViewBag.userAge = HttpContext.Session.GetString(userAge);

            var movieFiles = from movies in _context.Films
                             select movies;

            if (!string.IsNullOrEmpty(userInput))
            {
                movieFiles = movieFiles.Where(value1 => value1.movieTitle.Contains(userInput));
            }

            if (!string.IsNullOrEmpty(movieCert))
            {
                movieFiles = movieFiles.Where(value2 => value2.movieCertificate == movieCert);
            }

            var movieCertFound = _context.Films.Select(value1 => value1.movieCertificate).Distinct();

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movieFiles = movieFiles.Where(value3 => value3.movieGenre == movieGenre);
            }

            var movieGenreFound = _context.Films.Select(value1 => value1.movieGenre).Distinct();

            List<Films> filmModel = movieFiles.ToList();
            ViewData["userInput"] = userInput;
            ViewData["sortMovieCert"] = movieCert;
            ViewData["movieCertFound"] = movieCertFound.ToList();
            ViewData["movieCertSList"] = new SelectList(movieCertFound.ToList());
            ViewData["sortMovieGenre"] = movieGenre;
            ViewData["movieGenreFound"] = movieGenreFound.ToList();
            ViewData["movieGenreSList"] = new SelectList(movieGenreFound.ToList());
            return View(filmModel);
        }

        [HttpGet]
        public IActionResult ShoppingCart()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewShoppingCart()
        {
            List<MovieShoppingCart> shoppingCart = new List<MovieShoppingCart>();

            if(HttpContext.Session.GetString(userCart) != null)
            {
                string JSONserialized = HttpContext.Session.GetString(userCart);
                shoppingCart = JsonSerializer.Deserialize<List<MovieShoppingCart>> (JSONserialized);
            }

            if(TempData["msg"] != null)
            {
                ViewBag.msg = TempData["msg"].ToString();
            }
            return View(shoppingCart);
        }

        [HttpPost]
        public IActionResult RemoveFromShoppingCart(IFormCollection movieForm)
        {
            var shoppingCartList = new List<MovieShoppingCart>();
            int movieID = int.Parse(movieForm["movieID"]);

            if(HttpContext.Session.GetString(userCart) != null)
            {
                string JSONserialized = HttpContext.Session.GetString(userCart);
                shoppingCartList = JsonSerializer.Deserialize<List<MovieShoppingCart>>(JSONserialized);
                var newMovie = shoppingCartList.FirstOrDefault(movie => movie.movieID == movie.movieID);

                if (newMovie != null)
                {
                    shoppingCartList.Remove(newMovie);
                }

            }
            HttpContext.Session.SetString(userCart, JsonSerializer.Serialize(shoppingCartList));
            TempData["msg"] = "Movie has been removed";
            return RedirectToAction("ViewShoppingCart");
        }

        [HttpPost]
        public IActionResult PurchaseFromShoppingCart(IFormCollection movieForm)
        {
            var shoppingCartList = new List<MovieShoppingCart>();
            int movieID = int.Parse(movieForm["movieID"]);

            if (HttpContext.Session.GetString(userCart) != null)
            {
                string JSONserialized = HttpContext.Session.GetString(userCart);
                shoppingCartList = JsonSerializer.Deserialize<List<MovieShoppingCart>>(JSONserialized);
                var newMovie = shoppingCartList.FirstOrDefault(movie => movie.movieID == movie.movieID);

                if (newMovie != null)
                {
                    shoppingCartList.Remove(newMovie);
                    ViewBag.Message = string.Format("Movie has been purchased!");
                }

            }
            HttpContext.Session.SetString(userCart, JsonSerializer.Serialize(shoppingCartList));
            TempData["msg"] = "Movie has been purchased!";
            return RedirectToAction("ViewShoppingCart");
        }

        [HttpGet]
        public IActionResult ViewGallery()
        {
            List<Films> filmModel = _context.Films.ToList();
            return View(filmModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
