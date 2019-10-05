using Common.StateMachines;
using SamuraiGame.Player.States;

namespace SamuraiGame.Player
{
    public class PlayerStateMachine : StateMachine<PlayerController, PlayerStateBase>
    {
        public override PlayerStateBase DefaultState => new IdleState();

        public void Update()
        {
            CurrentState.Update();
        }
    }
}