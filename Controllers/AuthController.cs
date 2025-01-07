using ExpenseApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace blogging_platform_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ExpenseContext _context;
        private readonly PasswordHasher<UserD> _hasher;
        private readonly IConfiguration _configuration;

        public AuthController(ExpenseContext context, PasswordHasher<UserD> hasher, IConfiguration configuration)
        {
            _context = context;
            _hasher = hasher;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IResult> RegisterUser(UserD user) 
        {
            if(user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password)) 
            {
                return TypedResults.BadRequest("Invalid user data");
            }
            
            var userExists = await FindUserByEmail(user.Email);

            if(userExists != null)
            {
                return TypedResults.BadRequest("email already exists");
            }
            
            var newUser = new User
            {
                CreatedDateAt = DateTime.Now,
                Email = user.Email,
                PasswordHash = _hasher.HashPassword(user, user.Password)
            };
          
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return TypedResults.Ok("For now this would suffice");
        }

        [HttpPost("login")]
        public async Task<IResult> Login(UserD user)
        {
            if(user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password)) 
            {
                return TypedResults.BadRequest("Invalid user data");
            }

            var userExists = await FindUserByEmail(user.Email);

            if(userExists == null)
            {
                return TypedResults.BadRequest("email doesn't exist in the DB");
            }

            if(_hasher.VerifyHashedPassword(user, userExists.PasswordHash, user.Password) > 0)
            {
                //make JWT token and give back to user
                var token = GenerateJwtToken(user.Email);


                return TypedResults.Ok(token);
            }
            else 
            {
                return TypedResults.Ok("Wrong password please try again!");
            }
        }

        private string GenerateJwtToken(string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> FindUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == email);

            return user;
        }

    }
}