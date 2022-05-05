using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using AspNet.Tutorial.Core.DataTransfers.Auths;
using AspNet.Tutorial.Core.Entities;
using AspNet.Tutorial.Core.Exceptions;
using AspNet.Tutorial.Core.Interfaces.IRepositories;
using AspNet.Tutorial.Core.Interfaces.IServices;
using AspNet.Tutorial.WebApi.Utils;

using AutoMapper;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AspNet.Tutorial.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<AccessTokenDto> VerifyLogin(string email, string password)
        {
            User user = await _userRepository.GetByEmail(email);

            if (user == null || !user.Password.Equals(password))
            {
                throw new AuthenticationException("Password does not match");
            }

            return new AccessTokenDto {AccessToken = GenerateAccessToken(user), RefreshToken = GenerateRefreshToken(user)};
        }

        public async Task<User> Register(UserRegisterDto userRegisterDto)
        {
            User user = _mapper.Map<User>(userRegisterDto);
            return await _userRepository.Insert(user);
        }

        public async Task<AccessTokenDto> RefreshToken(string token)
        {
            var id = JwtUtil.GetGuid(token);
            User user = await _userRepository.Get(id);

            if (user == null)
            {
                throw new RecordNotFoundException("User not in database");
            }

            return new AccessTokenDto {AccessToken = GenerateAccessToken(user), RefreshToken = GenerateRefreshToken(user)};
        }

        private string GenerateAccessToken(User user)
        {
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            SigningCredentials credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken tokenOptions = new JwtSecurityToken(
                "https://localhost:5001",
                "https://localhost:5001",
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials,
                claims: new[] {new Claim("id", user.Id.ToString()), new Claim("email", user.Email)});

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private string GenerateRefreshToken(User user)
        {
            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            SigningCredentials credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken tokenOptions = new JwtSecurityToken(
                "https://localhost:5001",
                "https://localhost:5001",
                expires: DateTime.Now.AddDays(10),
                signingCredentials: credentials,
                claims: new[] {new Claim("id", user.Id.ToString())});

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
