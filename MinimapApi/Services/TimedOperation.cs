using System.Diagnostics;

namespace MinimapApi.Services;

public sealed class TimedOperation : IDisposable
{
    private readonly ILogger logger;
    private readonly LogLevel logLevel;
    private readonly string messageTemplate;
    private readonly object?[] args;
    private readonly long startingTimestamp;

    public TimedOperation(ILogger logger, LogLevel logLevel, string messageTemplate, object?[] args)
    {
        this.logger = logger;
        this.logLevel = logLevel;
        this.messageTemplate = messageTemplate;
        this.args = new object[args.Length + 1];
        Array.Copy(args, this.args, args.Length);
        this.startingTimestamp = Stopwatch.GetTimestamp();
    }

    public void Dispose()
    {
        var delta = Stopwatch.GetElapsedTime(startingTimestamp);
        args[^1] = delta.TotalMilliseconds;
        logger.Log(logLevel, $"{messageTemplate} completed in {{OperationDurationMs}}ms", args);
    }
}