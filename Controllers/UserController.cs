using IntegrifyAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IntegrifyAssignment.Controllers
{
    [Route("api/user")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly UserContext _context;
        public UserController(IConfiguration config, UserContext context)
        {
            _context = context;
            _configuration = config;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> getUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
    }
}
