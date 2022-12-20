using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Entities;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class UserController : Controller
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IMapper _mapper;



        public UserController(DatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            List<User> users = _databaseContext.Userss.ToList();
            List<UserModel> model = users.Select(x => _mapper.Map<UserModel>(x)).ToList(); //kullanıcı bilgilerini listeleme
            return View(users);
        }
    }
}
