using System.Collections.Generic;

namespace Mgfirefox.CrisisTd
{
    public static class HashSetExtension
    {
        public static ReadOnlySet<T> AsReadOnly<T>(this ISet<T> self)
        {
            return new ReadOnlySet<T>(self);
        }
    }
}
