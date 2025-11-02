using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Mgfirefox.CrisisTd
{
    public class MapNotLoadedException : NotLoadedException
    {
        private const string type = "Map";

        public MapNotLoadedException() : base(type)
        {
        }

        protected MapNotLoadedException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public MapNotLoadedException(Exception innerException) : base(type, innerException)
        {
        }
    }
}
