using Microsoft.AspNetCore.Mvc;
using IntegrifyAssignment.Models;
using CryptSharp;
using IntegrifyAssignment.Interface;

namespace IntegrifyAssignment.Controllers
{
    [Route("api/signup")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly IUsers _IUsers;

        public SignupController(IUsers iusers)
        {
            _IUsers = iusers;
        }
        [HttpPost]
        public async Task<ActionResult<string>> PostUser(string email, string password)
        {
            User user = new User();

            if(_IUsers.GetUserByEmail(email) != null)
            {
                return Problem("User for " + email + " already exists");
            }
            user.email = email;
            user.datecreated = DateTime.UtcNow;
            user.dateupdated = DateTime.UtcNow;
            user.password = Crypter.Blowfish.Crypt(password);
            _IUsers.AddUser(user);

            return CreatedAtAction(nameof(PostUser), new { id = user.id }, user);
        }
    }
}
