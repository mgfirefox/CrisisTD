using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Mgfirefox.CrisisTd
{
    public class CaughtUnexpectedCheckedException : Exception
    {
        private const string message = "Unexpected checked exception has been caught.";

        public CaughtUnexpectedCheckedException()
        {
        }

        protected CaughtUnexpectedCheckedException([NotNull] SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public CaughtUnexpectedCheckedException(Exception innerException) : base(message,
            innerException)
        {
        }
    }
}
