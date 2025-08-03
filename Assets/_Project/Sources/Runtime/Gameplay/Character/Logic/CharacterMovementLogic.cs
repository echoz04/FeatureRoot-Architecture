using Sources.Core.Features.Logic;
using UnityEngine;

namespace Sources.Runtime.Gameplay.Character.Logic
{
    public sealed class CharacterMovementLogic : ILogic
    {
        public Vector2 Velocity { get; private set; }

        private readonly Rigidbody2D _rigidbody2D;
        private readonly float _moveSpeed;

        public CharacterMovementLogic(Rigidbody2D rigidbody2D, float moveSpeed)
        {
            _rigidbody2D = rigidbody2D;
            _moveSpeed = moveSpeed;
        }

        public void Move()
        {
            Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            var normalizedDirection = inputDirection.normalized;

            Velocity = normalizedDirection * _moveSpeed;

            _rigidbody2D.linearVelocity = Velocity;
        }
    }
}