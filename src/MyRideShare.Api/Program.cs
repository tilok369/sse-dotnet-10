using MyRideShare.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<RideStatusService>();
builder.Services.AddHostedService<RideShareBackgroundWorker>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapGet("/ride-status", (RideStatusService rideStatusService, CancellationToken cancellationToken) => 
    TypedResults.ServerSentEvents(rideStatusService.GetNextDirection(cancellationToken), eventType: "ride-status")
    );

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();