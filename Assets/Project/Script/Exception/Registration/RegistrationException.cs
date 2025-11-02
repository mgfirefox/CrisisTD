using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Mgfirefox.CrisisTd
{
    public class RegistrationException : Exception
    {
        public RegistrationException()
        {
        }

        protected RegistrationException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public RegistrationException(string message) : base(message)
        {
        }

        public RegistrationException(string message, Exception innerException) : base(message,
            innerException)
        {
        }
    }
}
