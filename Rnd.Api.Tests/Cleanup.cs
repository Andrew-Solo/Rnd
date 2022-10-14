namespace Rnd.Api.Tests;

public class Cleanup : IAsyncDisposable
{
    public Cleanup(Func<Task> cleanupFn)
    {
        _cleanupFn = cleanupFn;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        await _cleanupFn();
        
        _disposed = true;
    }


    private bool _disposed;
    private readonly Func<Task> _cleanupFn;
}