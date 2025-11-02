using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class TargetTypeValidator
    {
        public static IReadOnlySet<TargetType> ValidTypes { get; } = new HashSet<TargetType>
        {
            TargetType.Active,
            TargetType.Possible,
            TargetType.Excluded,
        };

        public static void Validate(TargetType type)
        {
            if (ValidTypes.Contains(type))
            {
                return;
            }

            throw new ValidationException(type.ToString(), "Target");
        }

        public static bool TryValidate(TargetType type)
        {
            try
            {
                Validate(type);

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
