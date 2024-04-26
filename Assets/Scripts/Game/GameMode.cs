using Characters.Controllers;
using Characters;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameMode : MonoBehaviour, IGameMode
    {
        private ICharacterController currentController;
        private Character playerCharacter;

        [Inject]
        public void Construct(PlayerController playerController, Character character)
        {
            playerCharacter = character;
            currentController = playerController;
            currentController.SetCharacter(character);
        }

        public void SwitchController(ICharacterController controller)
        {
            currentController.SetCharacter(null);
            currentController = controller;
            currentController.SetCharacter(playerCharacter);
        }
    }
}