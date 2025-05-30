using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyRideShare.Api;

public class RideStatusService: INotifyPropertyChanged
{
    public RideStatusService()
    {
        NextDirection = "Starting the trip";
    }

    private static List<string> _directions = ["left", "right", "straight"];
    private static List<string> _distances = ["50", "60", "70", "80", "90", "100"];
    public event PropertyChangedEventHandler? PropertyChanged;
    
    private string _nextDirection;
    
    private string NextDirection
    {
        get => _nextDirection;
        set
        {
            _nextDirection = value;
            OnPropertyChanged();
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void SetDirection()
    {
        NextDirection = $"Going {_directions[Random.Shared.Next(_directions.Count)]} for {_distances[Random.Shared.Next(_distances.Count)]} meters";
    }
    
    public async IAsyncEnumerable<string> GetNextDirection([EnumeratorCancellation] CancellationToken ct)
    {
        while (ct is not { IsCancellationRequested: true })
        {
            yield return NextDirection;
            var tcs = new TaskCompletionSource();
            PropertyChangedEventHandler handler = (_, _) => tcs.SetResult();
            PropertyChanged += handler;
            try
            {
                await tcs.Task.WaitAsync(ct);
            }
            finally
            {
                PropertyChanged -= handler;
            }
        }
    }
}