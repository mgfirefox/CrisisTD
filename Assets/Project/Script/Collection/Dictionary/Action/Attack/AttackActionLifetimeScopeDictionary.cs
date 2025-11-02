using System;

namespace Mgfirefox.CrisisTd
{
    [Serializable]
    public class AttackActionLifetimeScopeDictionary : SerializableDictionary<AttackActionType,
        AbstractAttackActionLifetimeScope>
    {
    }
}
