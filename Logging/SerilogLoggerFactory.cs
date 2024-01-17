using System;
using System.IO;
using Serilog;
using Serilog.Core;
using Serilog.Core.Enrichers;
using ILogger = tt.Tools.Logging.ILogger;
using ILoggerFactory = tt.Tools.Logging.ILoggerFactory;

namespace UNO_Spielprojekt.Logging;

public class SerilogLoggerFactory : ILoggerFactory
{
    private const string LoggerTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}";

    public ILogger CreateLogger(Type type)
    {
        var logger = CreateSerilogLogger(type.Name);
        return new SerilogLogger(logger);
    }

    public ILogger CreateLogger(string name)
    {
        var logger = CreateSerilogLogger(name);
        return new SerilogLogger(logger);
    }

    public ILogger CreateLogger(Type type, string fileName)
    {
        return CreateLogger(type);
    }

    public ILogger CreateLogger(string name, string fileName)
    {
        return CreateLogger(name);
    }

    private Logger CreateSerilogLogger(string name)
    {
        var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.With(new PropertyEnricher("SourceContext", name))
            .WriteTo.File(Path.Combine(AppContext.BaseDirectory, "log.txt"), retainedFileCountLimit:2, fileSizeLimitBytes:1 * 1024 * 1024, outputTemplate: LoggerTemplate, shared: true, rollOnFileSizeLimit: true)
            .WriteTo.Console(outputTemplate: LoggerTemplate)
            .CreateLogger();
        return logger;
    }
}