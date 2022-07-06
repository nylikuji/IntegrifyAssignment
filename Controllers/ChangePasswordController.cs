using Microsoft.AspNetCore.Mvc;
using IntegrifyAssignment.Models;
using IntegrifyAssignment.Interface;
using CryptSharp;
using Microsoft.AspNetCore.Authorization;

namespace IntegrifyAssignment.Controllers
{
    [Authorize]
    [Route("api/changepassword")]
    [ApiController]
    public class ChangePasswordController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IUsers _IUsers;

        public ChangePasswordController(UserContext context, IUsers iusers)
        {
            _context = context;
            _IUsers = iusers;
        }

        [HttpPut]
        public async Task<IActionResult> ChangePassword(string newpass, string token)
        { 
            long tokenID = _IUsers.GetIdByToken(token);
            User user = _IUsers.GetUserDetails(tokenID);
            user.password = Crypter.Blowfish.Crypt(newpass);
            user.dateupdated = DateTime.UtcNow;
            Console.WriteLine(user.email + " changed their password");
            _IUsers.UpdateUser(user);

            return Ok("password changed");
        }
    }
}
