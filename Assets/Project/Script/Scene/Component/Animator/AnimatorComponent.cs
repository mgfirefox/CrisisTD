using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class AnimatorComponent : AbstractComponent, IAnimatorComponent
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Animator animator;

        public void SetBool(string name, bool value)
        {
            animator.SetBool(name, value);
        }

        public void SetFloat(string name, float value)
        {
            animator.SetFloat(name, value);
        }

        public void SetInt(string name, int value)
        {
            animator.SetInteger(name, value);
        }

        public void ActivateTrigger(string name)
        {
            animator.SetTrigger(name);
        }
    }
}
