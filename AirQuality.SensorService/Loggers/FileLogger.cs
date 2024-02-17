﻿namespace AirQuality.SensorService.Loggers;

public class FileLogger : ILogger, IDisposable
{
    private readonly string filePath;
    private readonly static object _lock = new();
    public FileLogger(string path)
    {
        filePath = path;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return this;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        lock (_lock)
        {
            File.AppendAllText(filePath, formatter(state, exception) + Environment.NewLine);
        }
    }

    public void Dispose()
    {
        
    }
}
