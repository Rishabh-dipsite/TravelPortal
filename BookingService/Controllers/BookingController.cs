using BookingService.Repositories;
using BookingService.Services;
using Microsoft.AspNetCore.Mvc;
using SharedClassLibrary.Entities;
using SharedClassLibrary.Entities.Requests;

namespace BookingService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingRepository _repository;
        private readonly IProviderService _providerService;
        private readonly IUserService _userService;

        public BookingController(ILogger<BookingController> logger, IBookingRepository repository, IProviderService providerService, IUserService userService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger;
            _providerService = providerService ?? throw new ArgumentNullException(nameof(providerService));
            _userService = userService;
        }

        [HttpPost("create", Name = "CreateBooking")]
        public async Task<ActionResult<BookingItem>> CreateBooking(CreateBookingRequest bookingRequest)
        {
            DateOnly departureDate;
            if (!DateOnly.TryParse(bookingRequest.Date, out departureDate))
                return BadRequest("Invalid date!");
            //Price can be fixed here after checking the provider and giving a timer window to user to confirm.
            var booking = new BookingItem
            {
                BookingId = DateTime.Now.Ticks,
                UserId = Request.Headers["claims_userid"].ToString(),
                EntityId = bookingRequest.EntityId,
                From = bookingRequest.From,
                To = bookingRequest.To,
                Class = bookingRequest.Class,
                Date = bookingRequest.Date,
                Type = bookingRequest.Type,
                Price = "11000",
                Status = Status.Initialized,
                Provider = bookingRequest.Provider
            };
            _ = _userService.UpsertBooking(booking);
            _logger.LogInformation("Added booking to user service so it can be continued in case of consumers network or hardware failure.");
            return await _repository.CreateBooking(booking)? Ok(booking) : UnprocessableEntity();
        }

        [HttpPost("confirm", Name = "ConfirmBooking")]
        public async Task<ActionResult> ConfirmBooking(ConfirmBookingRequest confirmBookingRequest)
        {
            var booking = await _repository.GetBooking(confirmBookingRequest.BookingId);
            if (booking == null || booking.UserId != Request.Headers["claims_userid"].ToString())
                return BadRequest("Booking does not exists!");
            if (booking.Status != Status.Initialized) return UnprocessableEntity("Booking is either Confirmed or is Executing");

            var validationResult = await _providerService.Book(new ProviderBookRequest
            {
                BookingId = booking.BookingId,
                BookingDetails = booking,
                AddressLine = confirmBookingRequest.AddressLine,
                ZipCode = confirmBookingRequest.ZipCode,
                CardName = confirmBookingRequest.CardName,
                CardNumber = confirmBookingRequest.CardNumber,
                Country = confirmBookingRequest.Country,
                CVV = confirmBookingRequest.CVV,
                EmailAddress = confirmBookingRequest.EmailAddress,
                Expiration = confirmBookingRequest.Expiration,
                FirstName = confirmBookingRequest.FirstName,
                LastName = confirmBookingRequest.LastName,
                PaymentMethod = confirmBookingRequest.PaymentMethod,
                State = confirmBookingRequest.State
            });
            if(validationResult == "OK")
            {
                booking.Status = Status.Executing;
                _repository.UpdateBooking(booking);
                await _userService.UpdateBookingStatus(new UpdateBookingStatusRequest
                {
                    BookingId = booking.BookingId,
                    BookingStatus = Status.Executing,
                    UserId = booking.UserId,
                });
                return Accepted();
            }
            _logger.LogError("The booking was not valid at provider service. This means some provider checks like PEP checks have failed");
            return UnprocessableEntity(validationResult);
        }

        [HttpPost("shortlist")]
        public async Task<ActionResult> shortlist([FromBody]long bookingId)
        {
            var booking = await _repository.GetBooking(bookingId);
            if (booking == null || booking.UserId != Request.Headers["claims_userid"].ToString())
                return BadRequest("Booking does not exists!");
            if(await _userService.ShortlistBooking(booking)) return Ok();
            return BadRequest();
        }
    }
}