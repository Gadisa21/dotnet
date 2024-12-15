using System;

class Program
{
    static void Main(string[] args)
    {
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.OnStarted += MessageHandler;
        stopwatch.OnStopped += MessageHandler;
        stopwatch.OnReset += MessageHandler;

        Console.WriteLine("Press S to start the stopwatch.");
        Console.WriteLine("Press T to stop the stopwatch.");
        Console.WriteLine("Press R to reset the stopwatch.");
        Console.WriteLine("Press Q to quit.");

        while (true)
        {
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.S:
                    stopwatch.Start();
                    break;
                case ConsoleKey.T:
                    stopwatch.Stop();
                    break;
                case ConsoleKey.R:
                    stopwatch.Reset();
                    break;
                case ConsoleKey.Q:
                    return;
            }
        }
    }

    static void MessageHandler(string message)
    {
        Console.WriteLine(message);
    }
}