using Microsoft.EntityFrameworkCore;
using Ticketron.Data;
using Ticketron.Interfaces;
using Ticketron.Models;

namespace Ticketron.Repository
{
    public class ParticipantRepository : IParticipantRepository
    {
        private readonly DataContext _context;
        public ParticipantRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateParticipant(Participant participant)
        {
            _context.Add(participant);
            return Save();
        }

        public bool DeleteParticipant(Participant participant)
        {
            _context.Remove(participant);
            return Save();
        }

        public Participant? GetParticipant(int participantId)
        {
            return _context.Participants
                .Include(p => p.Booking)
                .Include(p => p.User)
                .Include(p => p.UnregUser)
                .FirstOrDefault(p => p.Id == participantId);
        }

        public ICollection<Participant> GetParticipants(int bookingId)
        {
            return _context.Participants
                .Include(p => p.Booking)
                .Include(p => p.User)
                .Include(p => p.UnregUser)
                .Where(p => p.Booking != null && p.Booking.Id == bookingId)
                .ToList();
        }

        public bool ParticipantExists(int participantId)
        {
            return _context.Participants.Any(p => p.Id == participantId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
