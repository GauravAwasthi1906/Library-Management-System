using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DataAccessLayer.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> op):base(op) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        // Entities
        public DbSet<Employee> employee { get; set; }
        public DbSet<Author> author{ get; set; }
        public DbSet<Book> book{ get; set; }
        public DbSet<Borrow> borrow{ get; set; }
        public DbSet<Category> category{ get; set; }
        public DbSet<Feedback> feedback{ get; set; }
        public DbSet<Fine> fine{ get; set; }
        public DbSet<Librarian> lirarian{ get; set; }
        public DbSet<Member> member{ get; set; }
        public DbSet<Publisher> publisher{ get; set; }
        public DbSet<Reservation> reservations{ get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Borrow>()
                .HasOne(b=>b.books)
                .WithMany(b=>b.borrow)
                .OnDelete(DeleteBehavior.Cascade);
            mb.Entity<Borrow>()
                .HasOne(m=>m.member)
                .WithMany(b=>b.borrow)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<Feedback>()
                .HasOne(m=>m.member)
                .WithMany(b=>b.feedback)
                .OnDelete(DeleteBehavior.Cascade);
                
            mb.Entity<Fine>()
                .HasOne(m=>m.member)
                .WithMany(f=>f.fine)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<Reservation>()
                .HasOne(m=>m.member)
                .WithMany(f=>f.reservation)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
