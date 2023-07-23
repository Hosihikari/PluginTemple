using System.Runtime.CompilerServices;

namespace PluginUnitTest;

internal abstract class TestItem
{
    public abstract void Start();

    //public virtual void Stop() { }

    public virtual bool? IsSuccess { get; private set; } = null;
    public virtual string FailedMessage { get; private set; } = string.Empty;

    public event Action<string, int>? OnSuccess;

    protected void MarkSuccess(
        [CallerFilePath] string? callerFilePath = null,
        [CallerLineNumber] int callerLineNumber = 0
    )
    {
        if (IsSuccess is true)
        {
            throw new Exception("MarkSuccess() called twice.");
        }

        if (IsSuccess is false)
        {
            throw new Exception("MarkSuccess() called after MarkFailed().");
        }
        IsSuccess = true;
        OnSuccess?.Invoke(callerFilePath ?? string.Empty, callerLineNumber);
    }

    public event Action<string, string, int>? OnFailed;

    protected void MarkFailed(
        string message,
        [CallerFilePath] string? callerFilePath = null,
        [CallerLineNumber] int callerLineNumber = 0
    )
    {
        if (IsSuccess is false)
        {
            throw new Exception("MarkFailed() called twice.");
        }

        if (IsSuccess is true)
        {
            throw new Exception("MarkFailed() called after MarkSuccess().");
        }
        IsSuccess = false;
        FailedMessage = message;
        OnFailed?.Invoke(message, callerFilePath ?? string.Empty, callerLineNumber);
    }

    public event Action<string, string, int>? OnWriteLine;
    public event Action<string, string, int>? OnWriteError;

    protected void WriteLine(
        string message,
        [CallerFilePath] string? callerFilePath = null,
        [CallerLineNumber] int callerLineNumber = 0
    )
    {
        OnWriteLine?.Invoke(message, callerFilePath ?? string.Empty, callerLineNumber);
    }

    protected void WriteError(
        string message,
        [CallerFilePath] string? callerFilePath = null,
        [CallerLineNumber] int callerLineNumber = 0
    )
    {
        OnWriteError?.Invoke(message, callerFilePath ?? string.Empty, callerLineNumber);
    }
}
