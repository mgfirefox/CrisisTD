using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class TargetPriorityValidator
    {
        public static IReadOnlySet<TargetPriority> ValidPriorities { get; } =
            new HashSet<TargetPriority>
            {
                TargetPriority.First,
                TargetPriority.Last,
                TargetPriority.Strongest,
                TargetPriority.Weakest,
                TargetPriority.Farthest,
                TargetPriority.Closest,
                TargetPriority.Random,
            };

        public static void Validate(TargetPriority priority)
        {
            if (ValidPriorities.Contains(priority))
            {
                return;
            }

            throw new ValidationException(priority.ToString(), "Target");
        }

        public static bool TryValidate(TargetPriority priority)
        {
            try
            {
                Validate(priority);

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
