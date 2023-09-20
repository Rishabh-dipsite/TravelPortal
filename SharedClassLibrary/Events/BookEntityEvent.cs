using SharedClassLibrary.Entities;

namespace SharedClassLibrary.Events
{
    public class BookEntityEvent
    {
        public BookingItem Booking { get; set; }
        public TravellerDetail TravellerInfo{ get; set; }
        public Guid TransactionId { get; set; }
        public bool Successful { get; set; }
    }
}