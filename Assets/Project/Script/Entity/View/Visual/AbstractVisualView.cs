using System;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public class AbstractVisualView : AbstractCollisionView, IVisualView
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private GameObject visual;

        [SerializeField]
        [BoxGroup("Visual")]
        [ReadOnly]
        private Vector3 pivotPoint;

        [SerializeField]
        [BoxGroup("Visual")]
        [ReadOnly]
        private bool isHidden;

        protected GameObject Visual => visual;

        private readonly IList<Renderer> renderers = new List<Renderer>();

        public Vector3 PivotPoint { get => pivotPoint; private set => pivotPoint = value; }

        public bool IsHidden { get => isHidden; private set => isHidden = value; }

        public event Action Showing;
        public event Action Hiding;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            PivotPoint = visual.transform.localPosition;

            renderers.AddRange(visual.GetComponentsInChildren<Renderer>());
        }

        public void Show()
        {
            if (!IsHidden)
            {
                return;
            }

            Showing?.Invoke();

            OnShowing();

            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = true;
            }

            IsHidden = false;
        }

        public void Hide()
        {
            if (IsHidden)
            {
                return;
            }

            Hiding?.Invoke();

            OnHiding();

            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = false;
            }

            IsHidden = true;
        }

        protected virtual void OnShowing()
        {
        }

        protected virtual void OnHiding()
        {
        }
    }
}
