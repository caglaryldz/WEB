using Microsoft.AspNetCore.Mvc;
using WebApplication99.Entities;
using WebApplication99.Helpers;
using WebApplication99.Models;
using AutoMapper;

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
        public IActionResult MemberListPartial()
        {
            List<MovieModel> movies =
                _databaseContext.Movies.ToList()
                    .Select(x => _mapper.Map<MovieModel>(x)).ToList();

            return PartialView("_MemberListPartial", movies);  //?
        }
    }
}
