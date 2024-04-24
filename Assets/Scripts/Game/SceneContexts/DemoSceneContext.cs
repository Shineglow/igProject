using Characters.Controllers;
using Zenject;

namespace Game.SceneContexts
{
    public class DemoSceneContext : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ICharacterController>().To<PlayerController>().AsSingle();
        }
    }
}