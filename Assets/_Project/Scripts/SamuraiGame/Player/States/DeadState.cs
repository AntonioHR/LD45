using System;
using Common.Audio;
using Common.Timers;
using SamuraiGame.Events;
using UnityEngine;

namespace SamuraiGame.Player.States
{
    public class DeadState : PlayerStateBase
    {
        bool done = false;
        protected override void Begin()
        {
            Player.Rigidbody.isKinematic = true;
            Player.Rigidbody.velocity = Vector2.zero;
        }
        public override void OnHitAnimationOver()
        {
            if (!done)
            {
                done = true;
                TriggerManager.Trigger(EventName.PlayerDeathAnimationStart);
                AudioManager.Instance.Play("player_dead");
                Player.animator.SetBool("dead", true);
                Player.deathParticle.Emit(1);
            }
        }

        public override void OnNoHealth()
        {
        }
    }
}
