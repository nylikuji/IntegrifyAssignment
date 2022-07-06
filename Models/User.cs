namespace IntegrifyAssignment.Models
{
    public class User
    {
        public long id { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public DateTime? datecreated { get; set; }
        public DateTime? dateupdated { get; set; }

        public string? JWTtoken { get; set; }
    }
}
