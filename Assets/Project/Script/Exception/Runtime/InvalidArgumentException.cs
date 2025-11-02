using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Mgfirefox.CrisisTd
{
    public class InvalidArgumentException : ArgumentException
    {
        private const string messageFormat = "Value {0} of argument with name {1} is not valid.";

        public InvalidArgumentException()
        {
        }

        protected InvalidArgumentException([NotNull] SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public InvalidArgumentException(string parameterName, string parameterValue) : base(
            string.Format(messageFormat, parameterValue, parameterName))
        {
        }

        public InvalidArgumentException(string parameterName, string parameterValue,
            Exception innerException) : base(
            string.Format(messageFormat, parameterValue, parameterName), innerException)
        {
        }
    }
}
