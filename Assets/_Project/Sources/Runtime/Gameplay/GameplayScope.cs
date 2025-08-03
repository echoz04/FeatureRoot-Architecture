using Sources.Runtime.Gameplay.Character;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Sources.Runtime.Gameplay
{
    public class GameplayScope : LifetimeScope
    {
        [SerializeField] private CharacterView _characterView;
        [SerializeField] private Rigidbody2D _characterRigidbody2D;
        [SerializeField] private float _characterMoveSpeed;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameplayFlow>();

            BuildCharacter(builder);
        }

        private void BuildCharacter(IContainerBuilder builder)
        {
            builder.Register<CharacterRoot>(Lifetime.Singleton)
                .AsSelf()
                .WithParameter(_characterRigidbody2D)
                .WithParameter(_characterMoveSpeed)
                .AsImplementedInterfaces();

            builder.RegisterComponent(_characterView)
                .AsImplementedInterfaces();
        }
    }
}