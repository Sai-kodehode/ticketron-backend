using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Ticketron.Models;

namespace Ticketron.Data
{
    public class DataContext:DbContext
    {


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { 


        }
        public DbSet<Booking> Bookings { get; set; }

        public DbSet<BookingParticipant> BookingParticipants { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Groupmember> Groupmembers { get; set; }

        public DbSet<Image> Images { get; set; }    

        public DbSet<Participant> Participants { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<TicketParticipant> TicketParticipants { get; set; }

        public DbSet<Unreguser> Unregusers { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BookingParticipant>()
                .HasKey(bp => new { bp.BookingId, bp.ParticipantId });
            modelBuilder.Entity<BookingParticipant>()
                .HasOne(b => b.Booking)
                .WithMany(bp => bp.BookingParticipants)
                .HasForeignKey(p => p.BookingId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookingParticipant>()
               .HasOne(p => p.Participant)
               .WithMany(bp => bp.BookingParticipants)
               .HasForeignKey(b => b.ParticipantId)
               .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<TicketParticipant>()
                .HasKey(tp => new { tp.TicketId, tp.ParticipantId });
            modelBuilder.Entity<TicketParticipant>()
                .HasOne(t => t.Ticket)
                .WithMany(tp=> tp.TicketParticipants)
                .HasForeignKey(p=>p.TicketId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TicketParticipant>()
                .HasOne(p => p.Participant)
                .WithMany(tp=>tp.TicketParticipants)
                .HasForeignKey(t=>t.ParticipantId)
                .OnDelete(DeleteBehavior.NoAction);





        }

    }
}
