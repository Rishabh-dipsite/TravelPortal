using SharedClassLibrary.Entities.Requests;

namespace BookingService.Services
{
    public class ProviderService: IProviderService
    {
        private readonly HttpClient _httpClient;
        public ProviderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> Book(ProviderBookRequest providerBookRequest)
        {
            var requestUrl = new Uri(_httpClient.BaseAddress + "/book");
            var response = await _httpClient.PostAsJsonAsync(requestUrl, providerBookRequest);
            if (!response.IsSuccessStatusCode) {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound) return "Unable to contact Provider";
                var error = await response.Content.ReadFromJsonAsync(typeof( HttpValidationProblemDetails)) as HttpValidationProblemDetails;
                return error?.Detail ?? "Unknown error occured";
            }
            return "OK";
        }

    }
}
