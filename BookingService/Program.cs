using BookingService.Repositories;
using BookingService.Services;
using Polly;
using Polly.Extensions.Http;
using Steeltoe.Discovery.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IBookingRepository, BookingRepository>();

builder.Services.AddHttpClient<IProviderService, ProviderService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Provider:API:url"]);
})
.SetHandlerLifetime(TimeSpan.FromMinutes(10))
.AddPolicyHandler(GetCircuitBreakerPolicy());

builder.Services.AddHttpClient<IUserService, UserService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["User:API:url"]);
})
.SetHandlerLifetime(TimeSpan.FromMinutes(10))
.AddPolicyHandler(GetCircuitBreakerPolicy());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDiscoveryClient(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(60));
}
