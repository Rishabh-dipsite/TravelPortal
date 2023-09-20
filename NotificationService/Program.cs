using MassTransit;
using NotificationService.EventConsumers;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient<IConsumer, BookEntityConsumer>(client =>
{
})
.SetHandlerLifetime(TimeSpan.FromMinutes(10))
.AddPolicyHandler(GetCircuitBreakerPolicy());
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<BookEntityConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(new Uri(builder.Configuration["RabbitMQ:Host"]));
        cfg.ReceiveEndpoint("book-entity-queue", e =>
        {
            e.ConfigureConsumer<BookEntityConsumer>(ctx);
        });
    });
});
//Bus.Factory.CreateUsingRabbitMq(cfg =>
//{
//    cfg.Host(new Uri(builder.Configuration["RabbitMQ:Host"]));
//    cfg.ReceiveEndpoint("book-entity-queue", e =>
//    {
//        e.Consumer<BookEntityConsumer>();
//    });
//});

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.


app.MapGet("/api/v1/HealthCheck", () => "OK");

app.Run();

IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(60));
}
