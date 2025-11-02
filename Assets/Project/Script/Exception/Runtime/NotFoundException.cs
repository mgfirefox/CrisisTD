using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Mgfirefox.CrisisTd
{
    public abstract class NotFoundException : Exception
    {
        private const string messageFormat = "{0} {1} with ID \"{2}\" has not been found.";

        protected NotFoundException()
        {
        }

        protected NotFoundException([NotNull] SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        protected NotFoundException(string type, string objectId, string objectType) : base(
            string.Format(messageFormat, objectType, type, objectId))
        {
        }

        protected NotFoundException(string type, string objectId, string objectType,
            Exception innerException) : base(
            string.Format(messageFormat, objectType, type, objectId), innerException)
        {
        }
    }
}
