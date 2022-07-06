using IntegrifyAssignment.Interface;
using IntegrifyAssignment.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace IntegrifyAssignment.Repository
{
    public class UserRepository : IUsers
    {
        private readonly UserContext _context;
        public IConfiguration _configuration;

        public UserRepository(IConfiguration config, UserContext context)
        {
            _context = context;
            _configuration = config;
        }
        public List<User> GetUserDetails()
        {
            try
            {
                return _context.Users.ToList();
            }
            catch
            {
                throw;
            }
        }

        public long GetIdByToken(string token)
        {
            foreach(User user in _context.Users)
            {
                if(user.JWTtoken == token)
                {
                    return user.id;
                }
            }

            return -1;
        }

        public User? GetUserByEmail(string email)
        {
            foreach (User u in _context.Users)
            {
                if (u.email == email)
                {
                    return u;
                }
            }

            return null;
        }

        public void SetUserToken(User user)
        {

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", user.id.ToString()),
                new Claim("email", user.email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            user.datecreated = user.datecreated;
            user.dateupdated = DateTime.UtcNow;
            user.JWTtoken = new JwtSecurityTokenHandler().WriteToken(token);

            UpdateUser(user);
        }

        public User GetUserDetails(long id)
        {
            try
            {
                User? user = _context.Users.Find(id);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateUser(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public User DeleteUser(long id)
        {
            try
            {
                User? user = _context.Users.Find(id);

                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                    return user;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool CheckUser(long id)
        {
            return _context.Users.Any(e => e.id == id);
        }
    }
}