using System;
using Common.StateMachines;
using UnityEngine;

namespace SamuraiGame.Player
{
    public class PlayerStateBase : State<PlayerController, PlayerStateBase>
    {
        protected PlayerController Player { get => Context; }

        public virtual void Update() { }
        public virtual void FixedUpdate() { }

        public virtual void OnDashPressed() { }

        public virtual void OnNoHealth()
        {
            UnityEngine.GameObject.Destroy(Context.gameObject);
        }

        public virtual void OnHit(Transform source)
        {
            
        }

        public virtual bool IsDashing()
        {
            return false;
        }
    }
}