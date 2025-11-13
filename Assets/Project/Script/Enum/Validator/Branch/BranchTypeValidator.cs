using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class BranchTypeValidator
    {
        public static IReadOnlySet<BranchType> ValidTypes { get; } = new HashSet<BranchType>
        {
            BranchType.Zero,
            BranchType.First,
            BranchType.Second,
        };

        public static void Validate(BranchType type)
        {
            if (ValidTypes.Contains(type))
            {
                return;
            }

            throw new ValidationException(type.ToString(), "Branch");
        }

        public static bool TryValidate(BranchType type)
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
