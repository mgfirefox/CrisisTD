using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Mgfirefox.CrisisTd
{
    public class CaughtUnexpectedException : Exception
    {
        private const string message = "Unexpected exception has been caught.";

        public CaughtUnexpectedException()
        {
        }

        protected CaughtUnexpectedException([NotNull] SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public CaughtUnexpectedException(Exception innerException) : base(message, innerException)
        {
        }
    }
}
