using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class TowerIdValidator
    {
        public static IReadOnlySet<TowerId> ValidIds { get; } = new HashSet<TowerId>
        {
            TowerId.SingleTargetAttackTest,
            TowerId.AreaAttackTest,
            TowerId.ArcAngleAttackTest,
            TowerId.MultipleTargetAttackTest,
            TowerId.ConstantBuffSupportTest,
        };

        public static void Validate(TowerId id)
        {
            if (ValidIds.Contains(id))
            {
                return;
            }

            throw new ValidationException(id.ToString(), "Tower");
        }

        public static bool TryValidate(TowerId id)
        {
            try
            {
                Validate(id);

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
