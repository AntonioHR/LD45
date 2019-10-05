using Common.Movement;
using UnityEngine;

namespace SamuraiGame.Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerStateMachine stateMachine = new PlayerStateMachine();

        public RigidbodyMoveChar MoveChar;

        private void Start()
        {
            stateMachine.Begin(this);
        }
        private void Update()
        {
            stateMachine.Update();
        }
    }
}