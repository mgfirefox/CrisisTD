using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public static class ContainerBuilderExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder RegisterDictionaryAsReadOnly<TKey, TValue>(
            this IContainerBuilder self, IReadOnlyDictionary<TKey, TValue> dictionary)
        {
            return self.RegisterInstance(dictionary).AsImplementedInterfaces(dictionary.GetType());
        }
    }
}
