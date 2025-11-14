using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class Mesh : AbstractUnitySceneObject, IMesh
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
