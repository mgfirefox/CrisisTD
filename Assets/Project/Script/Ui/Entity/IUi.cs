using System;

namespace Mgfirefox.CrisisTd
{
    public interface IUi : IUnitySceneObject
    {
        bool IsHidden { get; }

        event Action Showing;
        event Action Hiding;

        void Show();
        void Hide();
    }
}
