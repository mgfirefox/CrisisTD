using System.Runtime.CompilerServices;

namespace Mgfirefox.CrisisTd
{
    public static class objectExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Cast<T>(this object self)
        {
            if (self is T t)
            {
                return t;
            }

            throw new InvalidArgumentException(nameof(self), self?.ToString());
        }
    }
}
