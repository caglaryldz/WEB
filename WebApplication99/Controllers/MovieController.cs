using Microsoft.AspNetCore.Mvc;
using WebApplication99.Entities;
using WebApplication99.Helpers;
using WebApplication99.Models;
using AutoMapper;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication99.Entities;
using WebApplication99.Helpers;
using WebApplication99.Models;


namespace WebApplication99.Controllers
{
    public class MovieController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;
        private readonly IHasher _hasher;

        public MovieController(DatabaseContext databaseContext, IConfiguration configuration, IHasher hasher)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
            _hasher = hasher;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MovieListPartial()
        {

            List<Movie> movies = _databaseContext.Movies.ToList();
            List<MovieModel> model = new List<MovieModel>();
            _databaseContext.Movies.Select(x => new MovieModel { Id = x.Id, MovieName = x.MovieName, Yonetmen = x.Yonetmen /*, CreatedAt = x.CreatedAt, CategoryId = x.CategoryId */});


            return PartialView("_MovieListPartial", movies);  //?
        }

        public IActionResult AddNewMoviePartial()
        {
            return PartialView("_AddNewMoviePartial", new CreateMovieModel());
        }

        [HttpPost]
        public IActionResult AddNewMovie(CreateMovieModel model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.Movies.Any(x => x.MovieName.ToLower() == model.MovieName.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.MovieName), "Kullanici adi daha önceden alinmis.");
                    return PartialView("_AddNewMoviePartial", model);
                }

                Movie movie = new()
                {
                    MovieName = model.MovieName,
                    Yonetmen = model.Yonetmen,
                   
                };

                _databaseContext.Movies.Add(movie);
                _databaseContext.SaveChanges();
                int affectedRowCount = _databaseContext.SaveChanges();

                
                return PartialView("_AddNewMoviePartial", new CreateMovieModel { Done = "Film eklendi." });
            }

            return PartialView("_AddNewMoviePartial", model);
        }


    }
}
