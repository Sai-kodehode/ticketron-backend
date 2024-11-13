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
        public async Task<bool> CreateParticipantAsync(Participant participant)
        {
            await _context.AddAsync(participant);
            return await SaveAsync();
        }

        public async Task<bool> DeleteParticipantAsync(Participant participant)
        {
            _context.Remove(participant);
            return await SaveAsync();
        }

        public async Task<Participant?> GetParticipantAsync(Guid participantId)
        {
            return await _context.Participants
                .Include(p => p.Booking)
                .Include(p => p.User)
                .Include(p => p.UnregUser)
                .Include(p => p.Group)
                .FirstOrDefaultAsync(p => p.Id == participantId);
        }

        public async Task<ICollection<Participant>> GetParticipantsAsync(Guid bookingId)
        {
            return await _context.Participants
                .Include(p => p.Booking)
                .Include(p => p.User)
                .Include(p => p.UnregUser)
                .Include(p => p.Group)
                .Where(p => p.Booking != null && p.Booking.Id == bookingId)
                .ToListAsync();
        }

        public async Task<bool> ParticipantExistsAsync(Guid participantId)
        {
            return await _context.Participants.AnyAsync(p => p.Id == participantId);
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
