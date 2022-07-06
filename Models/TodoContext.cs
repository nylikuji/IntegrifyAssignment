using Microsoft.EntityFrameworkCore;


namespace IntegrifyAssignment.Models
{
    public class TodoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
           optionsBuilder.UseNpgsql("Host=localhost;Database=todolists;Username=postgres;Password=1234");
        }
        public virtual DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>(entity =>
            {
                entity.ToTable("todos", "public"); entity.Property(e => e.id).HasColumnName("id").HasDefaultValueSql("nextval('account.item_id_seq'::regclass)"); 
                entity.Property(e => e.name).IsRequired().HasColumnName("name");
                entity.Property(e => e.status).HasColumnName("status");
                entity.Property(e => e.datecreated).HasColumnName("datecreated");
                entity.Property(e => e.dateupdated).HasColumnName("dateupdated");
                entity.Property(e => e.description).HasColumnName("description");
                entity.Property(e => e.userid).HasColumnName("userid");
            }); modelBuilder.HasSequence("item_id_seq", "account");
        }
    }

}
