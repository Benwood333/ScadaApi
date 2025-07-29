using Microsoft.EntityFrameworkCore;
using ScadaApi.Models;


public class PointChangeSim : IHostedService, IDisposable
{
    private readonly IServiceProvider _services;
    private Timer? _timer;
    private readonly Random _random = new();

    public PointChangeSim(IServiceProvider services)
    {
        _services = services;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(ChangePointVals, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        return Task.CompletedTask;
    }

    private async void ChangePointVals(object? state)
    {
        var context = _services.CreateScope().ServiceProvider.GetRequiredService<RtuDbContext>();
        var points = await context.Points.ToListAsync();

        foreach (var point in points)
        {
            point.Value = _random.Next(0, 10);
            point.TimeStamp = DateTime.Now;
        }

        await context.SaveChangesAsync();

    }

    public Task StopAsync(CancellationToken stoppingToken)
    {

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
