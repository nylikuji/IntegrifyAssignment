using Microsoft.AspNetCore.Mvc;
using IntegrifyAssignment.Models;
using IntegrifyAssignment.Interface;
using CryptSharp;

namespace IntegrifyAssignment.Controllers
{
    [Route("api/signin")]
    [ApiController]
    public class SigninController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IUsers _IUsers;
        public SigninController(IConfiguration config, IUsers IUsers)
        {
            _configuration = config;
            _IUsers = IUsers;
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostSignin(string email, string password)
        {
            var user = new User();
            user = _IUsers.GetUserByEmail(email);

            if (user == null)
                return BadRequest();

            if (Crypter.CheckPassword(password, user.password))
            {
                _IUsers.SetUserToken(user);
                return Ok(user.JWTtoken);
            }

            return BadRequest("Invalid Credentials");
        }
    }
}
