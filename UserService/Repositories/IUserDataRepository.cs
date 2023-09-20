using SharedClassLibrary.Entities;
using UserService.Entities;

namespace UserService.Repositories
{
    public interface IUserDataRepository
    {
        public UserData? GetUserData(string userID);
        public string UpdateBookingStatus(string userId, long bookingId, Status status, RefundStatus? refundStatus = null);
        public bool UpsertBooking(string userId, BookingItem booking);
        public bool ShortlistBooking(string userId, BookingItem booking);
    }
}
