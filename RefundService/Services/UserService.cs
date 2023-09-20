using SharedClassLibrary.Entities.Requests;

namespace RefundService.Services
{
    public class UserService: IUserService
    {
        public HttpClient _httpClient { get; }
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> UpdateBookingStatus(UpdateBookingStatusRequest bookingStatus)
        {
            var requestUrl = new Uri(_httpClient.BaseAddress + "/updateBookingStatus");
            var response = await _httpClient.PostAsJsonAsync(requestUrl, bookingStatus);
            return response.IsSuccessStatusCode;
        }
    }
}
