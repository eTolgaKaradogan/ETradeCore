using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ETradeBusiness.Models;
using ETradeBusiness.Services.Bases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ETradeMvcWebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ILocationService _locationService;

        public AccountController(IAccountService accountService, ILocationService locationService)
        {
            _accountService = accountService;
            _locationService = locationService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            var model = new UserModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var account = _accountService.GetQuery().SingleOrDefault(u =>
                         u.UserName == user.UserName && u.Password == user.Password && u.Active);
                    if (account == null)
                    {
                        ViewBag.Message = "Invalid user name or password!";
                        return View(user);
                    }

                    List<Claim> claims =
                        new
                            List<Claim>() // Claimler kimlik kartınızda ad-soyad-d.tarihi falan yazar bu da öyle bir şey password'un burada olmaması lazım..
                        {
                               new Claim(ClaimTypes.Name, account.UserName),
                               new Claim(ClaimTypes.Role, account.Role.RoleName)
                            };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return RedirectToAction("Index", "Products");
                }

                return View(user);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Register()
        {
            try
            {
                ViewBag.Countries = new SelectList(_locationService.GetQuery().ToList(), "Id", "Name");
                var model = new UserModel();
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _accountService.Add(user);
                    return RedirectToAction(nameof(Login));  // nameof c# içinde class olabilir herhangi bir aksiyon olabilir burada Login dönecek...
                }

                return View(user);
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
