using System;
using Common.StateMachines;
using SamuraiGame.Player.States;
using UnityEngine;

namespace SamuraiGame.Player
{
    public class PlayerStateMachine : StateMachine<PlayerController, PlayerStateBase>
    {
        public override PlayerStateBase DefaultState => new IdleState();

        public void FixedUpdate()
        {
            CurrentState.FixedUpdate();
        }
        public virtual void Update()
        {
            CurrentState.Update();
        }

        public void OnDashPressed()
        {
            CurrentState.OnDashPressed();
        }

        public void OnNoHealth()
        {
            CurrentState.OnNoHealth();
        }

        public void OnHit(Transform source)
        {
            CurrentState.OnHit(source);
        }

        public bool IsDashing()
        {
            return CurrentState.IsDashing();
        }

        public void OnHitAnimationOver()
        {
            CurrentState.OnHitAnimationOver();
        }
    }
}