using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public static class MapIdValidator
    {
        public static IReadOnlySet<MapId> ValidIds { get; } = new HashSet<MapId>
        {
            MapId.Test,
        };

        public static void Validate(MapId id)
        {
            if (ValidIds.Contains(id))
            {
                return;
            }

            throw new ValidationException(id.ToString(), "Map");
        }

        public static bool TryValidate(MapId id)
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
