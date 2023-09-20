using SharedClassLibrary.Entities.Requests;

namespace RefundService.Services
{
    public interface IUserService
    {
        public Task<bool> UpdateBookingStatus(UpdateBookingStatusRequest bookingStatus);
    }
}
