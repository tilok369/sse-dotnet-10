namespace MyRideShare.Api;

public class RideShareBackgroundWorker(RideStatusService rideStatusService): BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            rideStatusService.SetDirection();
            await Task.Delay(1000, stoppingToken);
        }
    }
}