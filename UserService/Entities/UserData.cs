using SharedClassLibrary.Entities;

namespace UserService.Entities
{
    public class UserData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        // Only ID was necessary. But due to missing common DB we need to duplicate data on services
        public Dictionary<long, BookingItem> MyBooking { get; set; } = new Dictionary<long, BookingItem>();
        // Only ID was necessary. But due to missing common DB we need to duplicate data on services
        public Dictionary<long, BookingItem> Wishlist { get; set; } = new Dictionary<long, BookingItem>();
    }
}
