using DinoManga.Data;
using DinoManga.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using DinoManga.Data;
using DinoManga.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DinoManga.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly MangaDbContext _dbContext;

        public AuthController(IConfiguration configuration, MangaDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user == null)
            {
                return BadRequest("Invalid credentials");
            }

            var accessToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(user);

            // Lưu trạng thái đăng nhập vào Session
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Username", user.Username);

            return Ok("success");
        }

        [HttpPost("register")]
        public IActionResult Register(string username, string password, string role = "User")
        {
            if (_dbContext.Users.Any(u => u.Username == username))
            {
                return BadRequest("Username already exists");
            }

            var newUser = new User
            {
                Username = username,
                Password = password,
                Role = role
            };

            var accessToken = GenerateJwtToken(newUser);
            var refreshToken = GenerateRefreshToken(newUser);

            // Lưu trạng thái đăng nhập vào Session
            HttpContext.Session.SetString("UserId", newUser.Id.ToString());
            HttpContext.Session.SetString("Username", newUser.Username);

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();

            return Ok("Success");
        }
        [HttpPost("refresh")]
        public IActionResult RefreshToken(string refreshToken)
        {
            var user = ValidateRefreshToken(refreshToken);

            if (user == null)
            {
                return BadRequest("Invalid refresh token");
            }

            var accessToken = GenerateJwtToken(user);

            // Cập nhật Session với thông tin mới
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("Username", user.Username);

            return Ok(new { accessToken });
        }
        [HttpGet("user")]
        public IActionResult GetCurrentUser()
        {
            if (HttpContext.Session.TryGetValue("UserId", out var userId) &&
                HttpContext.Session.TryGetValue("Username", out var username))
            {
                return Ok(new { UserId = Encoding.UTF8.GetString(userId), Username = Encoding.UTF8.GetString(username) });
            }

            return Ok("Not logged in");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Kiểm tra xem UserId có trong Session không
            if (HttpContext.Session.TryGetValue("UserId", out var userId))
            {
                // Xóa trạng thái đăng nhập khỏi Session
                HttpContext.Session.Remove("UserId");
                HttpContext.Session.Remove("Username");

                return Ok("Logout successful");
            }

            return BadRequest("Not logged in");
        }
        [HttpGet("GetAllUser")]
       public IActionResult GetAllUser()
        {
            List<User> users = _dbContext.Users.ToList();

            return Ok(users);
        }



        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), 
                signingCredentials: creds
            );

            user.Token = new JwtSecurityTokenHandler().WriteToken(token);
            _dbContext.SaveChanges(); 

            return user.Token;
        }

        private string GenerateRefreshToken(User user)
        {
            var refreshToken = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                expires: DateTime.Now.AddDays(7),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:RefreshTokenKey"])),
                    SecurityAlgorithms.HmacSha256
                )
            ));

            // Kiểm tra xem refreshToken có giá trị hợp lệ không
            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                user.Token = refreshToken;
                _dbContext.SaveChanges();
            }

            return refreshToken;
        }



        private User ValidateRefreshToken(string refreshToken)
        {
            // Thực hiện logic kiểm tra refresh token trong cơ sở dữ liệu
            var user = _dbContext.Users.FirstOrDefault(u => u.Token == refreshToken);

            // Kiểm tra tính hợp lệ của refresh token
            if (user != null && IsValidRefreshToken(user, refreshToken))
            {
                return user;
            }

            return null;
        }

        private bool IsValidRefreshToken(User user, string refreshToken)
        {

            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jsonToken = handler.ReadToken(refreshToken) as JwtSecurityToken;

                if (jsonToken == null || jsonToken.ValidTo <= DateTime.Now)
                {
                    // Refresh token không hợp lệ vì đã hết hạn
                    return false;
                }

                // Kiểm tra xem refresh token có được ký bởi cùng một khóa bí mật hay không
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:RefreshTokenKey"])),
                    ValidateLifetime = true
                };

                SecurityToken securityToken;
                var principal = handler.ValidateToken(refreshToken, tokenValidationParameters, out securityToken);

                // Kiểm tra xem user id trong refresh token có khớp với user id của người dùng trong cơ sở dữ liệu hay không
                if (principal != null && principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier && c.Value == user.Id.ToString()))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }

            return false;
        }


    }
}
