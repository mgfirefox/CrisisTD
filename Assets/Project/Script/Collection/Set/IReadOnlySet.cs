using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public interface IReadOnlySet<T> : IReadOnlyCollection<T>
    {
        bool Contains(T item);
    }
}
