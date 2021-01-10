using JWTTest.Contracts;
using JWTTest.Models;
using JWTTest.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JWTTest.Services
{
    public interface IAuthenticationService
    {
        Task<string> AuthenticateUserAsync(UserDto user);
        Task RegisterUserAsync(UserDto user);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }

        public async Task<string> AuthenticateUserAsync(UserDto user)
        {
            Validate(user);

            var userEntity = await _userRepository.GetByUsername(user.Username);

            if (userEntity == null) throw new AuthenticationException($"Invalid user credentials");

            string password = GetPasswordHash(user.Password);

            if (password != userEntity.Password)
                throw new AuthenticationException("Invalid user credentials");

            return GenerateJSONWebToken(userEntity);
        }

        public async Task RegisterUserAsync(UserDto user)
        {
            Validate(user);

            var password = GetPasswordHash(user.Password);

            var userEntity = new UserLogin()
            {
                Id = 0,
                Username = user.Username,
                Password = password,
                UserTypeId = (int)Contracts.UserType.User
            };

            await _userRepository.Create(userEntity);
        }

        private string GetPasswordHash(string password)
        {
            // Security should honestly be better than this, but for this application it does the job just fine
            var sha1 = new SHA1CryptoServiceProvider();
            var bytes = Encoding.ASCII.GetBytes(password);
            var loginPasswordHash = sha1.ComputeHash(bytes);
            return Convert.ToBase64String(loginPasswordHash);
        }

        private void Validate(UserDto user)
        {
            if (string.IsNullOrEmpty(user.Password))
            {
                throw new AuthenticationException("User must have a valid password.");
            }

            if (string.IsNullOrEmpty(user.Username))
            {
                throw new AuthenticationException("User must have a valid username.");
            }
        }

        private string GenerateJSONWebToken(UserLogin userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                    new Claim(ClaimTypes.Name, userInfo.Username),
                    new Claim(ClaimTypes.Role, userInfo.UserType.Code)
                },
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
