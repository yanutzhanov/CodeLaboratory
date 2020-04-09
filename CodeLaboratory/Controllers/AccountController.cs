using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeLaboratory.Domain;
using CodeLaboratory.Models;
using CodeLaboratory.Services.Abstract;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CodeLaboratory.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersService _usersService;

        public AccountController(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            User user = await _usersService.GetUser(model.Login, model.Password);

            if (user is null)
                return BadRequest("Invalid username or password");

            await _usersService.Authenticate(user, HttpContext);

            return RedirectToAction("Me");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (_usersService.UserWithSameLoginIsExist(model.Login))
            {
                return BadRequest("User with same login already exist");
            }

            model.Role = Role.User;

            User user = model.Adapt<User>();

            await _usersService.Create(user);

            await _usersService.Authenticate(user, HttpContext);

            return RedirectToAction("Me");
        }

        [Authorize]
        public async Task<IActionResult> Me()
        {
            User user = await _usersService.GetAuthenticatedUser(User.Identity.Name);

            if (user is null) return BadRequest();

            return View(user);
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
