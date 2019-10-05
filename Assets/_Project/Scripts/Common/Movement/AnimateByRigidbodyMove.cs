using UnityEngine;

namespace Common.Movement
{
    [RequireComponent(typeof(RigidbodyMoveChar))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class AnimateByRigidbodyMove : MonoBehaviour
    {
        RigidbodyMoveChar mov;
        Animator animator;
        SpriteRenderer spr;

        private void Start()
        {
            mov = GetComponent<RigidbodyMoveChar>();
            animator = GetComponent<Animator>();
            spr = GetComponent<SpriteRenderer>();
        }
        private void Update()
        {
            animator.SetFloat("velocity_magnitude", mov.VelocityMag);

            spr.flipX = mov.FacingLeft;
        }
    }
}