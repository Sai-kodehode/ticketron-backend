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
        public DbSet<Image> Images { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<User> Tickets { get; set; }
        public DbSet<UnregUser> Unregusers { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
