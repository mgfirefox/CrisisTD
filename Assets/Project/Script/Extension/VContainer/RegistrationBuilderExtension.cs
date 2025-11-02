using System;
using System.Runtime.CompilerServices;
using VContainer;

namespace Mgfirefox.CrisisTd
{
    public static class RegistrationBuilderExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder AsImplementedInterfaces<TInterface>(
            this RegistrationBuilder self)
        {
            return self.AsImplementedInterfaces(typeof(TInterface));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder AsImplementedInterfaces(this RegistrationBuilder self,
            Type interfaceType)
        {
            Type[] interfaceTypes = interfaceType.GetInterfaces();

            return self.As(interfaceType).As(interfaceTypes);
        }
    }
}
