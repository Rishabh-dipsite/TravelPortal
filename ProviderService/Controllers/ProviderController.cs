using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ProviderService.DemoLogics;
using SharedClassLibrary.Entities.Requests;
using SharedClassLibrary.Events;

namespace ProviderService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProviderController : ControllerBase
    {

        private readonly ILogger<ProviderController> _logger;

        private readonly IPublishEndpoint _publishEndpoint;
        public ProviderController(ILogger<ProviderController> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;

        }

        //[HttpGet("book/{entityId}/{entityType}/{date}/{entityClass}", Name = "ValidateBooking")]
        [HttpPost("book", Name = "Booking")]
        public async Task<ActionResult> Book(ProviderBookRequest providerBookRequest)
        {
            var isValid = DummyValidation.ValidateEntity(providerBookRequest.BookingDetails.EntityId, providerBookRequest.BookingDetails.Type);
            if(isValid != String.Empty) return ValidationProblem(isValid);

            _logger.LogInformation("Validation passed.");
            //Fire and Forget..
            _ = DummyBooking.BookEntityOnThirdPartyAppBasedOnProvider(providerBookRequest, _publishEndpoint);
            return Ok();
        }
    }
}