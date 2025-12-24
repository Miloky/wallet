using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

public sealed class CustomFormatter : ConsoleFormatter, IDisposable
{
    private readonly IDisposable? _optionsReloadToken;

    public CustomFormatter() : base("customName")
    {
    }
    

    public override void Write<TState>(
        in LogEntry<TState> logEntry,
        IExternalScopeProvider? scopeProvider,
        TextWriter textWriter)
    {
        string? message =
            logEntry.Formatter?.Invoke(
                logEntry.State, logEntry.Exception);

        if (message is null)
        {
            return;
        }
        
        scopeProvider?.ForEachScope((scope, state) => state.WriteLine(scope), textWriter);
        
        logEntry.State.ToString();
        
        textWriter.WriteLine("MyTestMessage");
    }

    public void Dispose() => _optionsReloadToken?.Dispose();
}
