using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class WeaponAnimationHandler : MonoBehaviour
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Transform attackPoint;
        
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private AudioSource attackAudio;

        private void OnAnimationAttack()
        {
            attackAudio.Play();
            
            Tracer[] tracers = transform.parent.GetComponentsInChildren<Tracer>();
            foreach (Tracer tracer in tracers)
            {
                tracer.StartPosition = attackPoint.position;
                tracer.Enable();
            }
            
            Explosion[] explosions = transform.parent.GetComponentsInChildren<Explosion>();
            foreach (Explosion explosion in explosions)
            {
                explosion.Enable();
            }
        }
    }
}
