# StopwatchApp

## Description
A console application that can start, stop, and reset a stopwatch. The application uses events to notify the user when specific actions occur (e.g., stopwatch started, stopped, or reset). The UI actions (user inputs) are linked with event-handling methods using delegates.

## Features
- Start the stopwatch
- Stop the stopwatch
- Reset the stopwatch
- Real-time display of elapsed time
- Event notifications for start, stop, and reset actions

## Usage
1. Clone the repository or download the source code.
2. Open a terminal and navigate to the project directory.
3. Build the project using the following command:
    ```bash
    dotnet build
    ```
4. Run the application using the following command:
    ```bash
    dotnet run
    ```
5. Use the following keys to interact with the stopwatch:
    - Press `S` to start the stopwatch.
    - Press `T` to stop the stopwatch.
    - Press `R` to reset the stopwatch.
    - Press `Q` to quit the application.

## Code Structure
- `Program.cs`: Contains the main program logic and user interface.
- `Stopwatch.cs`: Contains the `Stopwatch` class with fields, methods, and events.

## Event Handlers
- `OnStarted`: Triggered when the stopwatch is started.
- `OnStopped`: Triggered when the stopwatch is stopped.
- `OnReset`: Triggered when the stopwatch is reset.

