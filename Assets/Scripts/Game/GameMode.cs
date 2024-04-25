using Characters.Controllers;
using UnityEngine;
using Zenject;

namespace Game
{
    public class GameMode : MonoBehaviour
    {
        private ICharacterController playerController;

        [Inject]
        public void Construct(ICharacterController playerController)
        {
            this.playerController = playerController;
        }
    }
}