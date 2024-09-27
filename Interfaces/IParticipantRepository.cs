using Ticketron.Models;

namespace Ticketron.Interfaces
{
    public interface IParticipantRepository
    {
        Participant? GetParticipant(int participantId);
        ICollection<Participant> GetParticipants(int bookingId);
        bool CreateParticipant(Participant participant);
        bool DeleteParticipant(Participant participant);
        bool Save();
        bool ParticipantExists(int participantId);
    }
}
