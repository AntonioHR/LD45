using System;
using UnityEngine;

namespace Common.Movement
{
    [CreateAssetMenu(menuName="2D/CharMover")]
    public class CharMover : ScriptableObject
    {
        #region Inspector Variables
        public float moveForce = 5.0f;
        public bool useSpeedCapFriction = false;
        public bool useBraking = false;
        public FrictionSettings frictionSettings;

        [Serializable]
        public class FrictionSettings
        {
            public static float StoppingThreshold = 1f;
            public float brakeForce = 4.0f;
            public float speedCap = 5f;
            public float speedCapFrictionPower = 5f;
        }

        #endregion



        private IMovingChar character;
        private Vector2 input;

        private Rigidbody2D rb{ get => character.Rigidbody; }


        public void DoFixedUpdate(IMovingChar character, Vector2 direction)
        {
            this.character = character;
            input = direction;
            FixedUpdate();
        }

        private void FixedUpdate()
        {            
            Move();
            if (useSpeedCapFriction || useBraking)
            {
                ApplyFriction();
            }
            CheckFacing();
        }

        private void CheckFacing()
        {
            if(input.magnitude != 0)
            {
                character.FacingDirection = input.normalized;
            }
        }

        private void Move()
        {

            rb.AddForce(input * moveForce, ForceMode2D.Force);
        }

        private void ApplyFriction()
        {
            if (rb.velocity.magnitude < FrictionSettings.StoppingThreshold && input == Vector2.zero)
            {
                FullStop();
            }
            else if (useBraking)
            {
                ApplyBrake();
            }

            if (useSpeedCapFriction && rb.velocity.magnitude > frictionSettings.speedCap)
            {
                rb.AddForce(-rb.velocity.normalized * frictionSettings.speedCapFrictionPower);
            }
        }

        private void FullStop()
        {
            rb.velocity = Vector2.zero;
        }

        private void ApplyBrake()
        {
            Vector2 brakeForce = -rb.velocity.normalized * frictionSettings.brakeForce;
            if (input.x != 0)
                brakeForce.x = 0;
            if (input.y != 0)
                brakeForce.y = 0;
            rb.AddForce(brakeForce);
        }
    }
}