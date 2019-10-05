using System;
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
            public float recoverTime = 1;

        }

        private Vector2 directionInput;
        private Configs configs;
        private Stopwatch recoverTimer = new Stopwatch();
        private bool dashOver = false;

        private Vector3 startPosition;
        public Vector2 TravelVector{ get => ( Player.transform.position - startPosition); }

        public DashState(Vector2 directionInput)
        {
            this.directionInput = directionInput;
        }

        protected override void Begin()
        {
            configs = Player.configs.dash;
            startPosition = Player.transform.position;
            Player.SpriteRenderer.color = Color.red;

            Player.Rigidbody.velocity = Vector2.zero;
        }

        public override void Update()
        {
            if(!dashOver && TravelVector.magnitude > configs.dashDistance)
            {
                dashOver = true;
                recoverTimer = Stopwatch.CreateAndStart();
                Player.SpriteRenderer.color = Color.magenta;
                
                Player.Rigidbody.velocity = Vector2.zero;
            }
            if(recoverTimer.ElapsedSeconds > configs.recoverTime)
                ExitTo(new IdleState());
        }
        protected override void End()
        {
            Player.SpriteRenderer.color = Color.white;
        }


        public override void FixedUpdate()
        {

            Player.dashMover.DoFixedUpdate(Player, dashOver ? Vector2.zero : directionInput);
        }
    }
}