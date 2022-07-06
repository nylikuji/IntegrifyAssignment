using Microsoft.EntityFrameworkCore;

namespace IntegrifyAssignment.Models
{
    public class UserContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=todolists;Username=postgres;Password=1234");
        }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users", "public"); entity.Property(e => e.id).HasColumnName("id").HasDefaultValueSql("nextval('account.item_id_seq'::regclass)"); 
                entity.Property(e => e.email).IsRequired().HasColumnName("email");
                entity.Property(e => e.password).IsRequired().HasColumnName("password");
                entity.Property(e => e.JWTtoken).HasColumnName("JWTtoken");
            }); modelBuilder.HasSequence("item_id_seq", "account");
        }
    }

}
