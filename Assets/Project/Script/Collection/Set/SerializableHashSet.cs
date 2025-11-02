using System;

namespace Mgfirefox.CrisisTd
{
    [Serializable]
    public class SerializableHashSet<T> : global::SerializableHashSet<T>, IReadOnlySet<T>
    {
    }
}
