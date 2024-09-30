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
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<UnregUser> UnregUsers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }




    }
}
