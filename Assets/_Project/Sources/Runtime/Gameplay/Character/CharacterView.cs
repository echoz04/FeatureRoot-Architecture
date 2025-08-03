using Sources.Core.Features.Gameplay.View;
using UnityEngine;

namespace Sources.Runtime.Gameplay.Character
{
    public class CharacterView : EntityView<CharacterRoot>
    {
        private void Start()
        {
            Root.OnMoved += SetMoveAnimation;
        }

        private void SetMoveAnimation(Vector2 moveDirection)
        {
            Debug.Log("direction is " + moveDirection);
        }

        private void OnDestroy()
        {
            Root.OnMoved -= SetMoveAnimation;
        }
    }
}