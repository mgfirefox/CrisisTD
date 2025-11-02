using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Mgfirefox.CrisisTd
{
    public class ValidationException : Exception
    {
        private const string messageFormat = "{0} ID \"{1}\" is not valid.";

        public ValidationException()
        {
        }

        protected ValidationException([NotNull] SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        public ValidationException(string id, string idType) : base(string.Format(messageFormat,
            idType, id))
        {
        }

        public ValidationException(string id, string idType, Exception innerException) : base(
            string.Format(messageFormat, idType, id), innerException)
        {
        }
    }
}
