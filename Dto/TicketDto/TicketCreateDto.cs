namespace Ticketron.Dto.TicketDto
{
    public class TicketCreateDto
    {
        public required string Title { get; set; }
        public required string Category { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset? PurchaseDate { get; set; }
        public int? Price { get; set; }
        public string? ImageUrl { get; set; }
        public Guid? PurchasedBy { get; set; }
        public Guid? AssignedUserId { get; set; }
        public Guid? AssignedUnregUserId { get; set; }
        public required Guid BookingId { get; set; }
    }
}
