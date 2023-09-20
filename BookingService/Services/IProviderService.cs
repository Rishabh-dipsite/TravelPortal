using SharedClassLibrary.Entities.Requests;

namespace BookingService.Services
{
    public interface IProviderService
    {
        Task<string> Book(ProviderBookRequest providerBookRequest);
    }
}
