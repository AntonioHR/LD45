using System;
using Common.Input;
using UnityEngine;

namespace Common.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RigidbodyMoveChar : MonoBehaviour
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



        Rigidbody2D rb;
        Vector2 input;
        Vector2 moveForceVec;
        bool facingLeft = false;

        public float VelocityMag { get { return rb.velocity.magnitude; } }
        public bool FacingLeft { get { return facingLeft; } }

        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            input = InputHelper.DefaultJoystickInputRawCapped;
            Move();
            if (useSpeedCapFriction || useBraking)
            {
                ApplyFriction();
            }

            CheckFacing();
        }

        private void CheckFacing()
        {
            if (input.x > 0)
                facingLeft = false;
            else if (input.x < 0)
                facingLeft = true;
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