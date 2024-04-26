using Characters.Controllers;

namespace Game
{
    public interface IGameMode
    {
        void SwitchController(ICharacterController controller);
    }
}