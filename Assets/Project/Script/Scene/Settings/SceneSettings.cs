using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    [CreateAssetMenu(fileName = "SceneSettings", menuName = "Settings/Scene")]
    public class SceneSettings : ScriptableObject
    {
        [SerializeField]
        [Required]
        private MathSettings mathSettings;

        public MathSettings MathSettings => mathSettings;
    }
}
