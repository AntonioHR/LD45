using UnityEngine;

namespace Common.Movement
{
    [RequireComponent(typeof(IMovingChar))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class AnimateByRigidbodyMove : MonoBehaviour
    {
        IMovingChar @char;
        Animator animator;
        SpriteRenderer spr;

        private void Start()
        {
            @char = GetComponent<IMovingChar>();
            animator = GetComponent<Animator>();
            spr = GetComponent<SpriteRenderer>();
        }
        private void Update()
        {
            animator.SetFloat("velocity_magnitude", @char.GetSpeed());

            spr.flipX = @char.FacingDirection.x < 0;
        }
    }
}