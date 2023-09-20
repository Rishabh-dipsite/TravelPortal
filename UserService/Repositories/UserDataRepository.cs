using SharedClassLibrary.Entities;
using UserService.Entities;

namespace UserService.Repositories
{
    public class UserDataRepository : IUserDataRepository
    {
        private readonly Dictionary<string, UserData> users = new Dictionary<string, UserData>
        {
            {"user1", new UserData{ ContactNo="9876543210", FirstName="Lorem", LastName="Lorem" } },
            {"user2", new UserData{ ContactNo="8765432109", FirstName="Lorem", LastName="Lorem" } },
            {"user3", new UserData{ ContactNo="7654321098", FirstName="Doler", LastName="Doler" } }
        };

        public bool UpsertBooking(string userId, BookingItem booking)
        {
            if (!users.ContainsKey(userId)) return false;
            users[userId].MyBooking[booking.BookingId] = booking;
            return true;
        }

        public UserData? GetUserData(string userID)
        {
            return users.ContainsKey(userID)? users[userID] : null;
        }

        public bool ShortlistBooking(string userId, BookingItem booking)
        {
            if (!users.ContainsKey(userId)) return false;
            users[userId].Wishlist[booking.BookingId] = booking;
            return true;
        }

        public string UpdateBookingStatus(string userId, long bookingId, Status status, RefundStatus? refundStatus = null)
        {
            if (!users.ContainsKey(userId)) return "User does not exists.";
            if (!users[userId].MyBooking.ContainsKey(bookingId)) return "Booking does not exist";
            users[userId].MyBooking[bookingId].Status = status;
            if (refundStatus != null) users[userId].MyBooking[bookingId].RefundStatus = refundStatus;
            return "OK";
        }
    }
}
