using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalAssesment.Controllers
{
    [Route("loginController")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private FinalAssesmentContext _context;
        private IConfiguration _config;

        public UserController(FinalAssesmentContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        private Credential AuthenticateUser(LoginCred cred)
        {
            Credential _cred = null;
            var user = _context.Credentials.SingleOrDefault(u => u.UserName == cred.UserName && u.Password == cred.Password);

            if (user != null)
            {
                _cred = new Credential { UserName = cred.UserName, Password = cred.Password, Role = user.Role };
            }
            return _cred;
        }

        private string GenerateToken(Credential cred)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, cred.UserName),
                new Claim(ClaimTypes.Role, cred.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost("login")]

        public IActionResult Login(LoginCred cred)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(cred);
            if (user != null)
            {
                var Token = GenerateToken(user);
                response = Ok(new { Token = Token });
            }
            return response;
        }

        [HttpPost("AddUser")]

        public async Task<ActionResult> AddUser([FromBody] Credential cred)
        {
            _context.Credentials.Add(cred);
            _context.SaveChanges();

            return Ok();
        }
    }
}
