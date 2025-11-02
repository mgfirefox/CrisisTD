using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Mgfirefox.CrisisTd
{
    public class SpawnFailedException : Exception
    {
        private const string messageFormat = "Failed to spawn {0} Object.";

        public SpawnFailedException()
        {
        }

        protected SpawnFailedException([NotNull] SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
        }

        public SpawnFailedException(string type) : base(string.Format(messageFormat, type))
        {
        }

        public SpawnFailedException(string type, Exception innerException) : base(
            string.Format(messageFormat, type), innerException)
        {
        }
    }
}
