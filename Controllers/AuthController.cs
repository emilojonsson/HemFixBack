using HemFixBack.Models;
using HemFixBack.Services;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Google.Apis.Auth;
using FirebaseAdmin.Auth;
using Newtonsoft.Json;

namespace HemFixBack.Controllers
{
    public class AuthController
    {
        public static IResult Login(UserLogin user, IUserService service, IConfiguration configuration)
        {
            if (!string.IsNullOrEmpty(user.Username) &&
                !string.IsNullOrEmpty(user.Password))
            {
                var loggedInUser = service.Get(user);
                if (loggedInUser is null) return Results.NotFound("User not found");

                var claims = new[]
                {
                  new Claim(ClaimTypes.NameIdentifier, loggedInUser.Username),
                  new Claim(ClaimTypes.Email, loggedInUser.EmailAddress),
                  new Claim(ClaimTypes.GivenName, loggedInUser.GivenName),
                  new Claim(ClaimTypes.Surname, loggedInUser.Surname),
                  new Claim(ClaimTypes.Role, loggedInUser.Role)
              };
                var jwtKey = Environment.GetEnvironmentVariable("JwtKey");
                var token = new JwtSecurityToken
              (
                  issuer: configuration["Jwt:Issuer"],
                  audience: configuration["Jwt:Audience"],
                  claims: claims,
                  expires: DateTime.UtcNow.AddDays(60),
                  notBefore: DateTime.UtcNow,
                  signingCredentials: new SigningCredentials(
                      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                      SecurityAlgorithms.HmacSha256)
              );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Results.Ok(tokenString);
            }
            return Results.BadRequest("Invalid user credentials");
        }
    }
}

