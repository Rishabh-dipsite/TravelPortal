using SharedClassLibrary.Entities;

namespace BookingService.Repositories
{
    public interface IBookingRepository
    {
        Task<BookingItem> GetBooking(long bookingId);
        Task<bool> CreateBooking(BookingItem booking);
        void UpdateBooking(BookingItem booking);
    }
}
