using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Mgfirefox.CrisisTd
{
    public class InvalidTowerConfigurationException : Exception
    {
        private const string messageFormat =
            "{0} Tower Configuration with ID \"{1}\" is not valid.";

        public InvalidTowerConfigurationException()
        {
        }

        protected InvalidTowerConfigurationException([NotNull] SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public InvalidTowerConfigurationException(string prefabId, string prefabType) : base(
            string.Format(messageFormat, prefabType, prefabId))
        {
        }

        public InvalidTowerConfigurationException(string prefabId, string prefabType,
            Exception innerException) : base(string.Format(messageFormat, prefabType, prefabId),
            innerException)
        {
        }
    }
}
