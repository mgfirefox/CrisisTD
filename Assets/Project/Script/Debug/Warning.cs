namespace Mgfirefox.CrisisTd
{
    public static class Warning
    {
        public static string RedundantRegistrationIgnoredMessage(string objectId, string objectType)
        {
            return
                $"Redundant registration of {objectType} with ID \"{objectId}\" has been ignored.";
        }

        public static string InvalidArgumentRestoredMessage(string argumentName,
            string argumentValue)
        {
            return $"Argument {argumentName} has been restored with value \"{argumentValue}\".";
        }

        public static string ObjectPropertyReplacedMessage(string objectType, string propertyName,
            string oldValue, string newValue)
        {
            return
                $"Property {propertyName} with value {oldValue} of Object of {objectType} is replaced with value {newValue}.";
        }
    }
}
