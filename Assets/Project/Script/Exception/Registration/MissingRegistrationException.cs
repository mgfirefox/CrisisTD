using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Mgfirefox.CrisisTd
{
    public class MissingRegistrationException : RegistrationException
    {
        private const string messageFormat = "Registration of {0} with ID \"{1}\" is missing.";

        public MissingRegistrationException()
        {
        }

        protected MissingRegistrationException([NotNull] SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public MissingRegistrationException(string objectId, string objectType) : base(
            string.Format(messageFormat, objectType, objectId))
        {
        }

        public MissingRegistrationException(string objectId, string objectType,
            Exception innerException) : base(string.Format(messageFormat, objectType, objectId),
            innerException)
        {
        }
    }
}
