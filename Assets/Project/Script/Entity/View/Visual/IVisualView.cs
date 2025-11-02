using System;
using UnityEngine;

namespace Mgfirefox.CrisisTd
{
    public interface IVisualView : ICollisionView
    {
        Vector3 PivotPoint { get; }

        bool IsHidden { get; }

        event Action Showing;
        event Action Hiding;

        void Show();
        void Hide();
    }
}
