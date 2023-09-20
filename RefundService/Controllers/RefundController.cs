using Microsoft.AspNetCore.Mvc;
using RefundService.Services;
using SharedClassLibrary.Entities;
using SharedClassLibrary.Entities.Requests;

namespace RefundService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RefundController : ControllerBase
    {
        private readonly ILogger<RefundController> _logger;
        private readonly IUserService _userService;

        public RefundController(ILogger<RefundController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost(Name = "InitiateRefund")]
        public async Task<ActionResult> InitiateRefund(InitiateRefundRequest refundRequest)
        {
            _logger.LogInformation("Refund is in progress");
            // Contact merchant bank and start reverting transaction.
            await Task.Delay(7000);

            await _userService.UpdateBookingStatus(new UpdateBookingStatusRequest
            {
                BookingId = refundRequest.BookingId,
                UserId = refundRequest.UserId,
                BookingStatus = Status.Failed,
                RefundStatus = RefundStatus.Completed
            });
            _logger.LogInformation("Refund completed");
            return Ok();
        }
    }
}