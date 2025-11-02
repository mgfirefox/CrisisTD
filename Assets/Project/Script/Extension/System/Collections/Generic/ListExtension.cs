using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Mgfirefox.CrisisTd
{
    public static class ListExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> self)
        {
            return new ReadOnlyCollection<T>(self);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<TAbstract> Cast<TAbstract, T>(this List<T> self)
            where T : TAbstract
        {
            return self.ConvertAll<TAbstract>(t => t);
        }
    }
}
