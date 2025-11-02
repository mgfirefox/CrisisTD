using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class AttackActionTypeValidator
    {
        public static IReadOnlySet<AttackActionType> ValidActionTypes { get; } =
            new HashSet<AttackActionType>
            {
                AttackActionType.SingleTarget,
                AttackActionType.Area,
                AttackActionType.ArcAngle,
                AttackActionType.Burst,
            };

        public static void Validate(AttackActionType actionType)
        {
            if (ValidActionTypes.Contains(actionType))
            {
                return;
            }

            throw new ValidationException(actionType.ToString(), "AttackAction");
        }

        public static bool TryValidate(AttackActionType actionType)
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
