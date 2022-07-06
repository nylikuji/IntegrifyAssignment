
namespace IntegrifyAssignment.Models
{
    public enum state
    {
        notstarted,
        ongoing,
        complete
    }
    public class Todo
    {
        public string? name { get; set; }
        public long id { get; set; }

        public string? status { get; set; }
        public DateTime? datecreated { get; set; }
        public DateTime? dateupdated { get; set; }
        public string? description { get; set; }

        public long? userid { get; set; }
    }
}
