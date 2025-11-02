using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Mgfirefox.CrisisTd
{
    public class RestorableInvalidArgumentException : InvalidArgumentException
    {
        public string ParameterName { get; } = "";

        public RestorableInvalidArgumentException()
        {
        }

        protected RestorableInvalidArgumentException([NotNull] SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public RestorableInvalidArgumentException(string parameterName, string parameterValue) :
            base(parameterName, parameterValue)
        {
            ParameterName = parameterName;
        }

        public RestorableInvalidArgumentException(string parameterName, string parameterValue,
            Exception innerException) : base(parameterName, parameterValue, innerException)
        {
            ParameterName = parameterName;
        }
    }
}
