using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WasteProject.Models;

namespace WasteProject.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginC(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                if (login.EMail == "admin@test.com" && login.Password == "123456")
                {
                    var claims = new List<Claim>
                    {
                      new Claim(ClaimTypes.Name, "Admin"),
        
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, 
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddHours(48)
                    };
 
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                         new ClaimsPrincipal(claimsIdentity));

 
                    return Ok("Ok");
                }
                else
                {
                    return Ok("Mail ya da Şifreniz Yanlış");
                }
            }
            else
            {
                return Ok("Eksik Bilgi Girdiniz");
            }
        }
        
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Index");
        }
    }
}
