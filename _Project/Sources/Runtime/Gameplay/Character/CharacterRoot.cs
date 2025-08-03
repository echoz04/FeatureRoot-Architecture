using System;
using Sources.Core.Features.Gameplay.Root;
using Sources.Runtime.Gameplay.Character.Logic;
using UnityEngine;
using VContainer.Unity;

namespace Sources.Runtime.Gameplay.Character
{
    public class CharacterRoot : BaseGameplayRoot, IFixedTickable
    {
        public event Action<Vector2> OnMoved;

        private readonly Rigidbody2D _rigibody2D;
        private readonly float _moveSpeed;

        private CharacterMovementLogic _movementLogic;
        private CharacterAttackLogic _attackLogic;

        public CharacterRoot(Rigidbody2D rigidbody2D, float moveSpeed)
        {
            _rigibody2D = rigidbody2D;
            _moveSpeed = moveSpeed;
        }

        public override void InitializeLogic()
        {
            _movementLogic = new CharacterMovementLogic(_rigibody2D, _moveSpeed);
            _attackLogic = new CharacterAttackLogic();

            AddLogic(_movementLogic, _attackLogic);
        }

        public void FixedTick()
        {
            Move();
        }

        private void Move()
        {
            _movementLogic.Move();

            OnMoved?.Invoke(_movementLogic.Velocity);
        }
    }
}