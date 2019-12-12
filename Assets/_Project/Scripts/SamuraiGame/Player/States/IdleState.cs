using Common.Input;
using UnityEngine;

namespace SamuraiGame.Player.States
{
    public class IdleState : PlayerStateBase
    {

        protected override void Begin()
        {

        }
        public override void FixedUpdate()
        {
            Player.defaultMover.DoFixedUpdate(Player, Player.DirectionInput);
        }
        public override void OnDashPressed()
        {
            ExitTo(new DashState(Player.configs.canDashInPlace? Player.DirectionInput : Player.FacingDirection));
        }

    }
}