using System;

namespace Mgfirefox.CrisisTd
{
    [Serializable]
    public class BuffActionLifetimeScopeDictionary : SerializableDictionary<BuffActionType,
        AbstractBuffActionLifetimeScope>
    {
    }
}
