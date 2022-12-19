using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Security.Claims;
using WebApplication2.Entities;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;

        public AccountController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string hashedPassword = DoMD5HashedString(model.Password);

                User user = _databaseContext.Userss.SingleOrDefault(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == hashedPassword);//kullanıcı adı ve sifre eslesme kontrolü

                if (user != null)
                {
                    if (user.Locked)
                    {
                        ModelState.AddModelError(nameof(model.Username), "Kullanıcı kilitli.");
                        return View(model);
                    }

                    List<Claim> claims = new List<Claim>();  //kullanıcı bilgilerini tutar
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())); //kullanici adini tutar
                    claims.Add(new Claim(ClaimTypes.Name, user.FullName ?? string.Empty)); //kullanici ad soyadi tutar
                    claims.Add(new Claim(ClaimTypes.Role, user.Role)); //kullanici rolünü tutar
                    claims.Add(new Claim("Username", user.Username)); //kullaniciAdi tutar

                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme); //identity ile sistemde dogrulanan kullanicinin sınırları belirlenir,
                                                                                                                             //cookie authentication kullanılacagı belirtilir

                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);  

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");//home sayfasına yönlendir
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya sifre yanlis.");
                }
            }

            return View(model);
        }

        private string DoMD5HashedString(string s)
        {
            string md5Salt = _configuration.GetValue<string>("AppSettings:MD5Salt");  //sifre tuzlama
            string salted = s + md5Salt;
            string hashed = salted.MD5();
            return hashed;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_databaseContext.Userss.Any(x => x.Username.ToLower() == model.Username.ToLower()))  //veritabanında kullanıcıdan gelen kullanıcı adı var mı
                {
                    ModelState.AddModelError(nameof(model.Username), "Kullanıcı adı daha önceden alınmıstır");
                    View(model);
                }

                string hashedPassword = DoMD5HashedString(model.Password); //md5 sifreleme

                User user = new()
                {
                    Username = model.Username,
                    Password = hashedPassword  //sifreleme
                };

                _databaseContext.Userss.Add(user);
                int affectedRowCount = _databaseContext.SaveChanges();

                if (affectedRowCount == 0)
                {
                    ModelState.AddModelError("", "User can not be added.");
                }
                else
                {
                    return RedirectToAction(nameof(Login));
                }
            }

            return View(model);
        }

        public IActionResult Profile()
        {
            ProfileInfoLoader();

            return View();
        }

        private void ProfileInfoLoader()
        {
            Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = _databaseContext.Userss.SingleOrDefault(x => x.Id == userid);

            ViewData["FullName"] = user.FullName;
        }

        [HttpPost]
        public IActionResult ProfileChangeFullName([Required][StringLength(50)] string? fullname)
        {
            if (ModelState.IsValid)
            {
                Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _databaseContext.Userss.SingleOrDefault(x => x.Id == userid);

                user.FullName = fullname;
                _databaseContext.SaveChanges();

                return RedirectToAction(nameof(Profile));
            }

            ProfileInfoLoader();
            return View("Profile");
        }

        [HttpPost]
        public IActionResult ProfileChangePassword([Required][MinLength(6)][MaxLength(16)] string? password)
        {
            if (ModelState.IsValid)
            {
                Guid userid = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _databaseContext.Userss.SingleOrDefault(x => x.Id == userid);

                string hashedPassword = DoMD5HashedString(password);

                user.Password = hashedPassword;
                _databaseContext.SaveChanges();

                ViewData["result"] = "PasswordChanged";
            }

            ProfileInfoLoader();
            return View("Profile");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login)); //cikis yapinca login sayfasina yönlendir.
        }
    }
}
