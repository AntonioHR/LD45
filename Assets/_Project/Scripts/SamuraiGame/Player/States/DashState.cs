using System;
using Common.Audio;
using Common.Timers;
using UnityEngine;

namespace SamuraiGame.Player.States
{
    public class DashState : PlayerStateBase
    {
        [Serializable]
        public class Configs
        {
            public float dashDistance = 3;
            public float recoverInvicibility = .3f;
            public float recoverTime = 1.2f;

            public float dashVel = 10;
            public float driftVel = 2;
        }

        private Vector2 directionInput;
        private Configs configs;
        private Stopwatch dashTimer = new Stopwatch();
        private Stopwatch recoverTimer = new Stopwatch();
        private bool dashOver = false;

        private Vector3 startPosition;
        private float dashDuration;

        public Vector2 TravelVector{ get => ( Player.transform.position - startPosition); }

        public DashState(Vector2 directionInput)
        {
            this.directionInput = directionInput;
        }

        protected override void Begin()
        {
            configs = Player.configs.dash;
            startPosition = Player.transform.position;
            dashDuration = configs.dashDistance / configs.dashVel;

            Player.dashHitbox.enabled = true;

            Player.animator.SetTrigger("dash");

            CheckFootDirection();

            Player.dashParticle.Emit(1);
            AudioManager.Instance.Play("dash");
            dashTimer.Reset();

            Player.Rigidbody.velocity = directionInput * configs.dashVel;
            // Player.Rigidbody.velocity = Vector2.zero;
        }

        public override void Update()
        {
            if(!dashOver && dashTimer.ElapsedSeconds >= dashDuration)
            {
                dashOver = true;
                recoverTimer = Stopwatch.CreateAndStart();

                Player.Rigidbody.velocity = directionInput * configs.driftVel;
            }
            if(recoverTimer.ElapsedSeconds > configs.recoverTime)
                ExitTo(new IdleState());
        }
        protected override void End()
        {
            Player.animator.ResetTrigger("blocked");
            Player.dashHitbox.enabled = false;
            Player.Rigidbody.velocity = Vector2.zero;
        }

        public override bool IsDashing()
        {
            return recoverTimer.ElapsedSeconds < configs.recoverInvicibility;
        }
    }
}