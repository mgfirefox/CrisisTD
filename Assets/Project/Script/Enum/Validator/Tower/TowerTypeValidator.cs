using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class TowerTypeValidator
    {
        public static IReadOnlySet<TowerType> ValidTypes { get; } = new HashSet<TowerType>
        {
            TowerType.Attack,
            TowerType.Support,
        };

        public static void Validate(TowerType type)
        {
            if (ValidTypes.Contains(type))
            {
                return;
            }

            throw new ValidationException(type.ToString(), "Tower");
        }

        public static bool TryValidate(TowerType type)
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
