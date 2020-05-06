using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CodeLaboratory.Data.Repositories.Abstract;
using CodeLaboratory.Domain;
using CodeLaboratory.Enteties;
using CodeLaboratory.Helpers;
using CodeLaboratory.Services.Abstract;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace CodeLaboratory.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        }

        public async Task Create(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            user.Password = MD5Algorithm.GetHashString(user.Password);

            await _usersRepository.Create(user.Adapt<UserEntity>());
        }

        public async Task<bool> UserIsExist(string login, string password)
        {
            if (string.IsNullOrEmpty(login)) throw new ArgumentNullException(nameof(login));
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

            return await _usersRepository.UserIsExist(login, password);
        }

        public bool UserWithSameLoginIsExist(string login)
        {
            return _usersRepository.UserWithSameLoginIsExist(login);
        }

        public string GetEncodedJwt(ClaimsIdentity identity)
        {
            if (identity == null) throw new ArgumentNullException(nameof(identity));

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: DateTime.UtcNow,
                claims: identity.Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                                                   SecurityAlgorithms.HmacSha256));

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public async Task<User> GetAuthenticatedUser(string login)
        {
            if (string.IsNullOrEmpty(login)) throw new ArgumentNullException(nameof(login));
            UserEntity foundUser = await _usersRepository.GetUserByLogin(login);

            if (foundUser is null) return null;

            User user = foundUser.Adapt<User>();
            user.Projects = foundUser.UserProjects.Select(up => up.Project.Adapt<Project>());

            return user;
        }

        public async Task<User> GetUser(string login, string password)
        {
            if (string.IsNullOrEmpty(login)) throw new ArgumentNullException(nameof(login));
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

            password = CodeLaboratory.Helpers.MD5Algorithm.GetHashString(password);

            UserEntity foundUser = await _usersRepository.GetUser(login, password);

            User user = foundUser.Adapt<User>();
               
            return user;
        }

        public ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        public async Task Authenticate(User user, HttpContext context)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        } 
    }
}
