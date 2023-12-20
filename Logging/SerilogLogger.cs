using System;

namespace UNO_Spielprojekt.Logging;
using ILogger = tt.Tools.Logging.ILogger;

public class SerilogLogger : ILogger
{
    private readonly Serilog.Core.Logger logger;

    public SerilogLogger(Serilog.Core.Logger logger)
    {
        this.logger = logger;
    }

    public void Debug(string message)
    {
        logger.Debug(message);
    }

    public void Debug(Func<string> messageFactory)
    {
        logger.Debug(messageFactory.Invoke());
    }

    public void Debug(Exception ex, string message)
    {
        logger.Debug(ex, message);
    }

    public void Info(string message)
    {
        logger.Information(message);
    }

    public void Info(Func<string> messageFactory)
    {
        logger.Information(messageFactory.Invoke());
    }

    public void Info(Exception ex, string message)
    {
        logger.Information(ex, message);
    }

    public void Warn(string message)
    {
        logger.Warning(message);
    }

    public void Warn(Func<string> messageFactory)
    {
        logger.Warning(messageFactory.Invoke());
    }

    public void Warn(Exception ex, string message)
    {
        logger.Warning(ex, message);
    }

    public void Error(string message)
    {
        logger.Error(message);
    }

    public void Error(Func<string> messageFactory)
    {
        logger.Error(messageFactory.Invoke());
    }

    public void Error(Exception ex, string message)
    {
        logger.Error(ex, message);
    }
}