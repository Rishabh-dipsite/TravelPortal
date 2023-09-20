namespace SharedClassLibrary.Entities.Requests
{
    public class ProviderBookRequest: ConfirmBookingRequest
    {
        public BookingItem BookingDetails { get; set; }
    }
}
