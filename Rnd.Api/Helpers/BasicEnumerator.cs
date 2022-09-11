using System.Collections;

namespace Rnd.Api.Helpers;

public class BasicEnumerator<T> : IEnumerator<T> where T : notnull
{
    public BasicEnumerator(params T[] items)
    {
        _items = items;
    }

    public T Current => _items[_index];
    
    public bool MoveNext()
    {
        _index++;
        return _index < _items.Length;
    }

    public void Reset() => _index = -1;

    private int _index = -1;
    private readonly T[] _items;
    
    object IEnumerator.Current => Current;

    void IDisposable.Dispose() { }
}