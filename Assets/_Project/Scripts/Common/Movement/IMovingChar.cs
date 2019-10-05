using UnityEngine;

namespace Common.Movement
{
    public interface IMovingChar
    {
        Rigidbody2D Rigidbody { get; }
        Vector2 FacingDirection { get; set; }
    }
    public static class MovingCharHelpers
    {
        public static float GetSpeed(this IMovingChar movingChar)
        {
            return movingChar.Rigidbody.velocity.magnitude;
        }
    }
}