using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Mgfirefox.CrisisTd
{
    public abstract class AbstractUi : AbstractUnitySceneObject, IUi
    {
        [SerializeField]
        [BoxGroup("Dependencies")]
        [Required]
        private Image panel;

        [SerializeField]
        [BoxGroup("Ui")]
        [ReadOnly]
        private bool isHidden;

        protected Image Panel { get => panel; set => panel = value; }

        public bool IsHidden { get => isHidden; private set => isHidden = value; }

        public event Action Showing;
        public event Action Hiding;

        public void Show()
        {
            if (!IsHidden)
            {
                return;
            }

            Showing?.Invoke();

            OnShowing();

            gameObject.SetActive(true);

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

            gameObject.SetActive(false);

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
