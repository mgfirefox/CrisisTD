using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class BranchLevelValidator
    {
        public static void Validate(BranchLevel branchLevel)
        {
            if (branchLevel == null)
            {
                throw new InvalidArgumentException(nameof(branchLevel), null);
            }

            if (!BranchTypeValidator.TryValidate(branchLevel.Type))
            {
                // TODO: Change Exception
                throw new InvalidArgumentException(nameof(branchLevel), branchLevel.ToString());
            }

            if (branchLevel.Index < 0)
            {
                throw new InvalidArgumentException(nameof(branchLevel), branchLevel.ToString());
            }
        }

        public static bool TryValidate(BranchLevel branchLevel)
        {
            try
            {
                Validate(branchLevel);

                return true;
            }
            catch (Exception e)
            {
                // TODO: Change Exception
                if (e is not Exception)
                {
                    Debug.LogException(new CaughtUnexpectedException(e));
                }
            }

            return false;
        }
    }
}
