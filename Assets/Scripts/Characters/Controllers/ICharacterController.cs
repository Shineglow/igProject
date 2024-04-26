using UnityEngine.InputSystem;

namespace Characters.Controllers
{
    public interface ICharacterController
    {
        void SetCharacter(IControlable2D character);
        void SetInput(PlayerInput input);
    }
}