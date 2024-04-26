using Characters;
using Characters.Controllers;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.SceneContexts
{
    public class MonoInstallerDemo : MonoInstaller
    {
        [SerializeField] 
        private Character character;

        [SerializeField] 
        private PlayerInput playerInput;

        public override void InstallBindings()
        {
            Container.Bind<PlayerInput>().FromInstance(playerInput).AsSingle();
            BindCharacterAssociatedEntities();
            BindControllers();
        }

        private void BindCharacterAssociatedEntities()
        {
            Container.Bind<Character>().FromInstance(character).AsSingle();
        }

        private void BindControllers()
        {
            Container.Bind<ICharacterController>().To<PlayerController>().AsSingle();
            Container.Bind<PlayerController>().AsSingle();
        }
    }
}