namespace SharedClassLibrary.Entities.Requests
{
    public class UpdateBookingStatusRequest
    {
        public string UserId { get; set; }
        public long BookingId { get; set; }
        public Status BookingStatus { get; set; }
        public RefundStatus? RefundStatus { get; set; }
    }
}
