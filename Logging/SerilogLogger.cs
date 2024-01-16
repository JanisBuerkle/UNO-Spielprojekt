using System;
using ILogger = tt.Tools.Logging.ILogger;

namespace UNO_Spielprojekt.Logging;

public class SerilogLogger : ILogger
{
    private readonly Serilog.Core.Logger _logger;

    public SerilogLogger(Serilog.Core.Logger logger)
    {
        this._logger = logger;
    }

    public void Debug(string message)
    {
        _logger.Debug(message);
    }

    public void Debug(Func<string> messageFactory)
    {
        _logger.Debug(messageFactory.Invoke());
    }

    public void Debug(Exception ex, string message)
    {
        _logger.Debug(ex, message);
    }

    public void Info(string message)
    {
        _logger.Information(message);
    }

    public void Info(Func<string> messageFactory)
    {
        _logger.Information(messageFactory.Invoke());
    }

    public void Info(Exception ex, string message)
    {
        _logger.Information(ex, message);
    }

    public void Warn(string message)
    {
        _logger.Warning(message);
    }

    public void Warn(Func<string> messageFactory)
    {
        _logger.Warning(messageFactory.Invoke());
    }

    public void Warn(Exception ex, string message)
    {
        _logger.Warning(ex, message);
    }

    public void Error(string message)
    {
        _logger.Error(message);
    }

    public void Error(Func<string> messageFactory)
    {
        _logger.Error(messageFactory.Invoke());
    }

    public void Error(Exception ex, string message)
    {
        _logger.Error(ex, message);
    }
}