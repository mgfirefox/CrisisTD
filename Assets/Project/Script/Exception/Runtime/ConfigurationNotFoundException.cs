using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Mgfirefox.CrisisTd
{
    public class ConfigurationNotFoundException : NotFoundException
    {
        public ConfigurationNotFoundException()
        {
        }

        protected ConfigurationNotFoundException([NotNull] SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public ConfigurationNotFoundException(string configurationId, string configurationType) :
            base("Configuration", configurationId, configurationType)
        {
        }

        public ConfigurationNotFoundException(string configurationId, string configurationType,
            Exception innerException) : base("Configuration", configurationId, configurationType,
            innerException)
        {
        }
    }
}
