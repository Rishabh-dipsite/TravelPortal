using MassTransit;
using SharedClassLibrary.Entities;
using SharedClassLibrary.Entities.Requests;
using SharedClassLibrary.Events;

namespace ProviderService.DemoLogics
{
    // Some wrong running operation which we will not wait for and return response through message queue
    public static class DummyBooking
    {
        public async static Task BookEntityOnThirdPartyAppBasedOnProvider(ProviderBookRequest providerBookRequest, IPublishEndpoint publishEndpoint)
        {
            await Task.Delay(7000); //Some heavy operations and going out of network to transact and update seats/ rooms.

            bool success = true;
            // We will fail all Business and Super Deluxe bookings to test the negative scenario.
            if(providerBookRequest.BookingDetails.Class == "B" || providerBookRequest.BookingDetails.Class == "SD")   success = false;
            await publishEndpoint.Publish(new BookEntityEvent
            {
                Booking = providerBookRequest.BookingDetails,
                TravellerInfo = new TravellerDetail
                {
                    AddressLine = providerBookRequest.AddressLine,
                    Country = providerBookRequest.Country,
                    EmailAddress = providerBookRequest.EmailAddress,
                    FirstName = providerBookRequest.FirstName,
                    LastName = providerBookRequest.LastName,
                    State = providerBookRequest.State,
                    ZipCode = providerBookRequest.ZipCode
                },
                TransactionId = Guid.NewGuid(),
                Successful = success
            });
        }
    }
}
