using Sources.Core.Features.Gameplay.Root;
using Sources.Core.Features.UI.View;
using UnityEngine;
using VContainer;

namespace Sources.Core.Features.Gameplay.View
{
    public abstract class EntityView<TRoot> : BaseGameplayView where TRoot : IGameplayRoot
    {
        protected TRoot Root;

        [Inject]
        public void SetRoot(TRoot root) =>
            Root = root;
    }
}