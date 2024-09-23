using System.Security.Cryptography.X509Certificates;

namespace Ticketron.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public User User { get; set; }

        public ICollection<Groupmember> Groupmembers { get; set; }

   
    }
}
