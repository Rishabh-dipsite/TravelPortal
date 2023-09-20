namespace SharedClassLibrary.Entities.Requests
{
    public class ConfirmBookingRequest: TravellerDetail
    {
        public long BookingId { get; set; }

        // Payment
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public int PaymentMethod { get; set; }
    }
}
