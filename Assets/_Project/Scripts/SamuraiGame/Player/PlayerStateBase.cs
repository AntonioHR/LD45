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
            Player.OnDead();
        }

        public virtual void OnHit(Transform source)
        {
            var dist = Player.transform.position - source.transform.position;
            dist.z = 0;
            dist.Normalize();
            Player.Rigidbody.AddForce(10 * dist, ForceMode2D.Impulse);
        }

        public virtual bool IsDashing()
        {
            return false;
        }
    }
}