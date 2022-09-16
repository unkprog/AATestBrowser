using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace WATestBrowser
{
    public interface ILoggerOutput
    {
        void Log(string logRecord);
    }

    public class ServerLoggerOptions
    {
        public virtual string FilePath { get; set; }

        public virtual string FolderPath { get; set; }
    }

    // ServerLoggingLoggerProvider.cs
    [ProviderAlias("ServerLogging")]
    public class ServerLoggerProvider : ILoggerProvider
    {
        public readonly ServerLoggerOptions Options;

        public ServerLoggerProvider(IOptions<ServerLoggerOptions> _options)
        {
            Options = _options.Value;

            //if (!Directory.Exists(Options.FolderPath))
            //{
            //    Directory.CreateDirectory(Options.FolderPath);
            //}
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new ServerLogging(this);
        }

        public void Dispose()
        {
        }
    }

    public class ServerLogging : ILogger
    {
        public static ILoggerOutput? LoggerOutput = null;
        protected readonly ServerLoggerProvider _serverLoggerProvider;

        public ServerLogging([NotNull] ServerLoggerProvider serverLoggerProvider)
        {
            _serverLoggerProvider = serverLoggerProvider;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            //var fullFilePath = _serverLoggerProvider.Options.FolderPath + "/" + _serverLoggerProvider.Options.FilePath.Replace("{date}", DateTimeOffset.UtcNow.ToString("yyyyMMdd"));
            var logRecord = string.Format("{0} [{1}] {2} {3}", "[" + DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss+00:00") + "]", logLevel.ToString(), formatter(state, exception), exception != null ? exception.StackTrace : "");
            LoggerOutput?.Log(logRecord);
            //using (var streamWriter = new StreamWriter(fullFilePath, true))
            //{
            //    streamWriter.WriteLine(logRecord);
            //}
        }
    }
    public static class ServerLoggerExtensions
    {
        public static ILoggingBuilder AddServerLogger(this ILoggingBuilder builder, Action<ServerLoggerOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, ServerLoggerProvider>();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
