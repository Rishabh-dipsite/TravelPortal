using SharedClassLibrary.Entities;
using SharedClassLibrary.Entities.Requests;

namespace BookingService.Services
{
    public interface IUserService
    {
        public Task<bool> UpsertBooking(BookingItem booking);
        public Task<bool> UpdateBookingStatus(UpdateBookingStatusRequest bookingStatus);
        public Task<bool> ShortlistBooking(BookingItem bookingItem);
    }
}
