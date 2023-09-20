using MassTransit;
using SharedClassLibrary.Entities.Requests;
using SharedClassLibrary.Events;

namespace NotificationService.EventConsumers
{
    public class BookEntityConsumer : IConsumer<BookEntityEvent>
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public BookEntityConsumer(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<BookEntityEvent> context)
        {
            var requestUrl = new Uri(_configuration["User:API:URL"] + "/updateBookingStatus");
            // Send message emails to consumer and providers here..
            await _httpClient.PostAsJsonAsync(requestUrl, new UpdateBookingStatusRequest
            {
                BookingId = context.Message.Booking.BookingId,
                BookingStatus = context.Message.Successful? SharedClassLibrary.Entities.Status.Completed: SharedClassLibrary.Entities.Status.Failed,
                UserId = context.Message.Booking.UserId,
                RefundStatus = context.Message.Successful ? SharedClassLibrary.Entities.RefundStatus.NotDefined : SharedClassLibrary.Entities.RefundStatus.InProcess
            });
            if (!context.Message.Successful)
            {
                // If Failed refund initiated..
                var refundUrl = new Uri(_configuration["Refund:API:URL"]);
                await _httpClient.PostAsJsonAsync(refundUrl, new InitiateRefundRequest
                {
                    BookingId = context.Message.Booking.BookingId,
                    UserId = context.Message.Booking.UserId,
                    TransactionId = context.Message.TransactionId
                }); 
            }
            //var jsonMessage = JsonConvert.SerializeObject(context.Message);
            Console.WriteLine($"OrderCreated message: {context.Message}");
        }
    }
}
