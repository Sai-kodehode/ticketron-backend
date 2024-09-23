namespace Ticketron.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Title { get; set; }
   
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Participant Participant { get; set; }
        
        public ICollection<Image> Images { get; set; }

      



    }
}
