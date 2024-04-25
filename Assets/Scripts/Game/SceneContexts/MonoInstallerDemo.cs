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
            if(character == null)
                Debug.Log($"{nameof(character)} reference is null!");
            Container.Bind<IControlable2D>().FromInstance(character).AsSingle();
            if(character == null)
                Debug.Log($"{nameof(PlayerInput)} reference is null!");
            Container.Bind<PlayerInput>().FromInstance(playerInput).AsSingle();
            Container.Bind<ICharacterController>().To<PlayerController>().AsSingle();
        }
    }
}