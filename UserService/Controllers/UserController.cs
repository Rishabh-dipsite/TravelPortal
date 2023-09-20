using Microsoft.AspNetCore.Mvc;
using SharedClassLibrary.Entities;
using SharedClassLibrary.Entities.Requests;
using UserService.Entities;
using UserService.Repositories;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserDataRepository _userDataRepository;

        public UserController(ILogger<UserController> logger, IUserDataRepository userDataRepository)
        {
            _logger = logger;
            _userDataRepository = userDataRepository;
        }

        [HttpGet(Name = "GetUserData")]
        public ActionResult<UserData> Get()
        {
            var user = _userDataRepository.GetUserData(Request.Headers["claims_userid"].ToString());
            if (user == null)
            {
                _logger.LogError("Claims not recognized");
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("updateBookingStatus", Name = "UpdateBookingStatus")]
        public ActionResult UpdateBookingStatus(UpdateBookingStatusRequest bookingStatusPatch)
        {
            _logger.LogInformation(String.Format("Booking {0} status updated to {1} and refund status to {2}",
                bookingStatusPatch.BookingId, bookingStatusPatch.BookingStatus, bookingStatusPatch.RefundStatus));
            var error = _userDataRepository.UpdateBookingStatus(bookingStatusPatch.UserId, bookingStatusPatch.BookingId, bookingStatusPatch.BookingStatus, bookingStatusPatch.RefundStatus);
            if (error != "OK")
                return BadRequest(error);
            return Accepted();
        }

        [HttpPost("UpsertBooking", Name = "UpsertBooking")]
        public ActionResult UpsertBooking(BookingItem booking)
        {
            var success = _userDataRepository.UpsertBooking(booking.UserId, booking);
            if (!success)
            {
                _logger.LogWarning("Something went wrong while upserting booking.");
                return BadRequest();
            }
            return Accepted();
        }

        [HttpPost("shortlistBooking", Name = "ShortlistBooking")]
        public ActionResult ShortlistBooking(BookingItem booking)
        {
            var success = _userDataRepository.ShortlistBooking(booking.UserId, booking);
            if (!success)
                return BadRequest(success);
            return Accepted();
        }
    }
}