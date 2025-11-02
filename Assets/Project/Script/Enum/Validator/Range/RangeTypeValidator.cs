using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class RangeTypeValidator
    {
        public static IReadOnlySet<RangeType> ValidTypes { get; } = new HashSet<RangeType>
        {
            RangeType.EnemyTarget,
            RangeType.TowerTarget,
        };

        public static void Validate(RangeType type)
        {
            if (ValidTypes.Contains(type))
            {
                return;
            }

            throw new ValidationException(type.ToString(), "Range");
        }

        public static bool TryValidate(RangeType type)
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
