using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class MeshComponent : AbstractComponent, IMeshComponent
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private MeshFilter filter;
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private new MeshRenderer renderer;

        public void Show()
        {
            renderer.enabled = true;
        }

        public void Hide()
        {
            renderer.enabled = false;
        }
    }
}
