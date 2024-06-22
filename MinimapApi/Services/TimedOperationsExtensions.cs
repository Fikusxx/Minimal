namespace MinimapApi.Services;

public static class TimedOperationsExtensions
{
    private const LogLevel DefaultLogLevel = LogLevel.Information;

    public static IDisposable BeginTimedOperation(this ILogger logger, string messageTemplate, params object[] args)
    {
        var disposable = BeginTimedOperation(logger, DefaultLogLevel, messageTemplate, args);

        return disposable;
    }
    
    public static IDisposable BeginTimedOperation(this ILogger logger, LogLevel logLevel, string messageTemplate, params object[] args)
    {
        var disposable = new TimedOperation(logger, logLevel, messageTemplate, args);
        
        return disposable;
    }
}