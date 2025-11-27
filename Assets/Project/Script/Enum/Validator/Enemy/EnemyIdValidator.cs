using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class EnemyIdValidator
    {
        public static IReadOnlySet<EnemyId> ValidIds { get; } = new HashSet<EnemyId>
        {
            EnemyId.TestWalk,
            EnemyId.TestRun,
        };

        public static void Validate(EnemyId id)
        {
            if (ValidIds.Contains(id))
            {
                return;
            }

            throw new ValidationException(id.ToString(), "Enemy");
        }

        public static bool TryValidate(EnemyId id)
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
