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
            ExitTo(new DashState(Player.FacingDirection));
        }

        public override void OnHit(Transform source)
        {
            var dist = Player.transform.position - source.transform.position;
            dist.z = 0;
            dist.Normalize();
            Player.Rigidbody.AddForce(10 * dist, ForceMode2D.Impulse);
        }
    }
}