using Microsoft.EntityFrameworkCore;
using Ticketron.Models;

namespace Ticketron.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<UnregUser> UnregUsers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.CreatedBy)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<Group>()
                .HasOne(g => g.CreatedBy)
                .WithMany(u => u.Groups)
                .HasForeignKey(g => g.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
