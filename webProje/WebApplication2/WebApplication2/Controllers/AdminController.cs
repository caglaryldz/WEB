using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{

    [Authorize(Roles = "admin")]  //Sadece adminler admin sayfasına girebilir
    public class AdminController : Controller//mantıklı
    {
        //[Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
