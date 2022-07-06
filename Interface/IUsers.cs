using IntegrifyAssignment.Models;

namespace IntegrifyAssignment.Interface
{
    public interface IUsers
    {
        public List<User> GetUserDetails();
        public User GetUserDetails(long id);
        public void AddUser(User user);
        public void UpdateUser(User user);
        public User DeleteUser(long id);
        public bool CheckUser(long id);
        public void SetUserToken(User user);
        public User? GetUserByEmail(string email);
        public long GetIdByToken(string token);
    }
}
