using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class WeaponAnimationHandler : MonoBehaviour
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private AudioSource attackAudio;

        private void OnAnimationAttack()
        {
            attackAudio.Play();
        }
    }
}
