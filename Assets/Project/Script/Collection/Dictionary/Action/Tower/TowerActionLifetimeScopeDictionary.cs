using System;

namespace Mgfirefox.CrisisTd
{
    [Serializable]
    public class TowerActionLifetimeScopeDictionary : SerializableDictionary<
        TowerType, AbstractTowerActionLifetimeScope>
    {
    }
}
