using UnityEngine;

namespace Sources.Core.Features.UI.View
{
    public abstract class BaseUIView : MonoBehaviour, IUIView
    {
        public abstract void Show();

        public abstract void Hide();
    }
}
