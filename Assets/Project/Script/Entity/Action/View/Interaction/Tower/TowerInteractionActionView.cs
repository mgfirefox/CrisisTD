using NaughtyAttributes;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class TowerInteractionActionView : AbstractActionView, ITowerInteractionActionView
    {
        [SerializeField]
        [BoxGroup("Interaction")]
        [ReadOnly]
        private bool isInteracting;

        public bool IsInteracting { get => isInteracting; set => isInteracting = value; }
    }
}
