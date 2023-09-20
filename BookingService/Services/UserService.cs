using SharedClassLibrary.Entities;
using SharedClassLibrary.Entities.Requests;

namespace BookingService.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ShortlistBooking(BookingItem booking)
        {
            var requestUrl = new Uri(_httpClient.BaseAddress + "/shortlistBooking");
            var response = await _httpClient.PostAsJsonAsync(requestUrl, booking);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateBookingStatus(UpdateBookingStatusRequest bookingStatus)
        {
            var requestUrl = new Uri(_httpClient.BaseAddress + "/updateBookingStatus");
            var response = await _httpClient.PostAsJsonAsync(requestUrl, bookingStatus);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpsertBooking(BookingItem booking)
        {
            var requestUrl = new Uri(_httpClient.BaseAddress + "/upsertBooking");
            var response = await _httpClient.PostAsJsonAsync(requestUrl, booking);
            var test = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }
    }
}
