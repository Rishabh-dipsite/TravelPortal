using SharedClassLibrary.Entities.Requests;

namespace SharedClassLibrary.Entities
{
    public enum Status
    {
        Initialized,
        Executing,
        Completed,
        Failed
    }
    public enum RefundStatus
    {
        NotDefined,
        InProcess,
        Completed
    }
    public class BookingItem : CreateBookingRequest
    {
        public long BookingId { get; set; }
        public string UserId { get; set; }
        public string Price { get; set; }
        public Status Status { get; set; }
        public RefundStatus? RefundStatus { get; set; }
    }
}
