using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Mgfirefox.CrisisTd
{
    public class PrefabNotFoundException : NotFoundException
    {
        public PrefabNotFoundException()
        {
        }

        protected PrefabNotFoundException([NotNull] SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public PrefabNotFoundException(string prefabId, string prefabType) : base("Prefab",
            prefabId, prefabType)
        {
        }

        public PrefabNotFoundException(string prefabId, string prefabType, Exception innerException)
            : base("Prefab", prefabId, prefabType, innerException)
        {
        }
    }
}
