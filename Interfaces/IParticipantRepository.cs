using Ticketron.Models;

namespace Ticketron.Interfaces
{
    public interface IParticipantRepository
    {
        Task<Participant?> GetParticipantAsync(Guid participantId);
        Task<ICollection<Participant>> GetParticipantsAsync(Guid bookingId);
        Task<bool> CreateParticipantAsync(Participant participant);
        Task<bool> DeleteParticipantAsync(Participant participant);
        Task<bool> SaveAsync();
        Task<bool> ParticipantExistsAsync(Guid participantId);
    }
}
