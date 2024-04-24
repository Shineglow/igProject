using UnityEngine.InputSystem;

namespace Characters.Controllers
{
    public interface ICharacterController
    {
        void Construct(IControlable2D character, PlayerInput input);
        void SetCharacter(IControlable2D character);
        void SetInput(PlayerInput input);
    }
}