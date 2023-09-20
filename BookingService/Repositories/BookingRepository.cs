using SharedClassLibrary.Entities;

namespace BookingService.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        Dictionary<long, BookingItem> bookings = new Dictionary<long, BookingItem>();
        public async Task<bool> CreateBooking(BookingItem booking)
        {
            return bookings.TryAdd(booking.BookingId, booking);
        }

        public async Task<BookingItem?> GetBooking(long bookingId)
        {
            return bookings.ContainsKey(bookingId)? bookings[bookingId] : null;
        }

        public void UpdateBooking(BookingItem booking)
        {
            bookings[booking.BookingId] = booking;
        }
    }
}
