using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CodeLaboratory.Domain;
using CodeLaboratory.Models;
using CodeLaboratory.Services;
using CodeLaboratory.Services.Abstract;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace CodeLaboratory.API.Controllers
{
    [ApiController]
    [Route("api/{Controller}/{Action}")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginModel model)
        {
            User user = _usersService.GetUser(model.Login, model.Password);
            if (user is null)
            {
                return BadRequest("Invalid username or password");
            }

            ClaimsIdentity identity = _usersService.GetIdentity(user);

            string encodedJwt = _usersService.GetEncodedJwt(identity);

            TokenResponse response = new TokenResponse
            {
                Token = encodedJwt,
                Login = identity.Name
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Register([FromBody]RegisterModel model)
        {
            if (_usersService.UserWithSameLoginIsExist(model.Login))
            {
                return BadRequest("User with same login already exist");
            }

            model.Role = Role.User;

            User user = model.Adapt<User>();

            _usersService.Create(user);

            var identity = _usersService.GetIdentity(user);

            string encodedJwt = _usersService.GetEncodedJwt(identity);

            TokenResponse response = new TokenResponse
            {
                Token = encodedJwt,
                Login = identity.Name
            };

            return Ok(response);
        }

        [HttpGet]
        public IActionResult CheckLogin(string login)
        {
            return _usersService.UserWithSameLoginIsExist(login) ? (IActionResult) BadRequest() : Ok();
        }
    }
}
