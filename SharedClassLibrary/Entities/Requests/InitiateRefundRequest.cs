namespace SharedClassLibrary.Entities.Requests
{
    public class InitiateRefundRequest
    {
        public long BookingId { get; set; }
        public string UserId { get; set; }
        public Guid TransactionId { get; set; }
    }
}
