using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CodeLaboratory.Data.Repositories.Abstract;
using CodeLaboratory.Domain;
using CodeLaboratory.Enteties;
using CodeLaboratory.Helpers;
using CodeLaboratory.Services.Abstract;
using Mapster;
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

        public void Create(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            _usersRepository.Create(user.Adapt<UserEntity>());
        }

        public bool UserIsExist(string login, string password)
        {
            if (string.IsNullOrEmpty(login)) throw new ArgumentNullException(nameof(login));
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

            return _usersRepository.UserIsExist(login, password);
        }

        public bool UserWithSameLoginIsExist(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentException("Value cannot be null or empty.", nameof(login));

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

        public User GetUser(string login, string password)
        {
            if (string.IsNullOrEmpty(login)) throw new ArgumentNullException(nameof(login));
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

            UserEntity fundedUser = _usersRepository.GetUser(login, password);

            return fundedUser.Adapt<User>();
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
    }
}
