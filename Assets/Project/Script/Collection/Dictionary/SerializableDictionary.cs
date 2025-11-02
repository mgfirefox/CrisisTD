using System;
using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    [Serializable]
    public class SerializableDictionary<TKey, TValue> :
        global::SerializableDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
    {
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => base.Keys;

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => base.Values;
    }
}
