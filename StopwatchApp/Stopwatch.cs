using System;
using System.Timers;

public delegate void StopwatchEventHandler(string message);

public class Stopwatch
{
    private System.Timers.Timer _timer;
    private TimeSpan _timeElapsed;
    public bool IsRunning { get; private set; }

    public event StopwatchEventHandler OnStarted;
    public event StopwatchEventHandler OnStopped;
    public event StopwatchEventHandler OnReset;

    public Stopwatch()
    {
        _timer = new System.Timers.Timer(1000);
        _timer.Elapsed += Tick;
        _timeElapsed = TimeSpan.Zero;
        IsRunning = false;
    }

    public void Start()
    {
        if (!IsRunning)
        {
            _timer.Start();
            IsRunning = true;
            OnStarted?.Invoke("Stopwatch Started!");
        }
    }

    public void Stop()
    {
        if (IsRunning)
        {
            _timer.Stop();
            IsRunning = false;
            OnStopped?.Invoke("Stopwatch Stopped!");
        }
    }

    public void Reset()
    {
        _timer.Stop();
        _timeElapsed = TimeSpan.Zero;
        IsRunning = false;
        OnReset?.Invoke("Stopwatch Reset!");
    }

    private void Tick(object sender, ElapsedEventArgs e)
    {
        _timeElapsed = _timeElapsed.Add(TimeSpan.FromSeconds(1));
        Console.WriteLine($"Time Elapsed: {_timeElapsed}");
    }
}