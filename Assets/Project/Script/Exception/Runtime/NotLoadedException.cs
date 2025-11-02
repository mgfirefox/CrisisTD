using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Mgfirefox.CrisisTd
{
    public class NotLoadedException : Exception
    {
        private const string messageFormat = "{0} is not loaded.";

        public NotLoadedException()
        {
        }

        protected NotLoadedException([NotNull] SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        public NotLoadedException(string objectType) : base(
            string.Format(messageFormat, objectType))
        {
        }

        public NotLoadedException(string objectType, Exception innerException) : base(
            string.Format(messageFormat, objectType), innerException)
        {
        }
    }
}
