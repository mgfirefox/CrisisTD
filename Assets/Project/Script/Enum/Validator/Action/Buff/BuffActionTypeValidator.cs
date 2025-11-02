using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class BuffActionTypeValidator
    {
        public static IReadOnlySet<BuffActionType> ValidActionTypes { get; } =
            new HashSet<BuffActionType>
            {
                BuffActionType.Constant,
            };

        public static void Validate(BuffActionType actionType)
        {
            if (ValidActionTypes.Contains(actionType))
            {
                return;
            }

            throw new ValidationException(actionType.ToString(), "BuffAction");
        }

        public static bool TryValidate(BuffActionType actionType)
        {
            try
            {
                Validate(actionType);

                return true;
            }
            catch (Exception e)
            {
                if (e is not ValidationException)
                {
                    Debug.LogException(new CaughtUnexpectedException(e));
                }
            }

            return false;
        }
    }
}
