using UnityEngine;

namespace SamuraiGame.Enemy.States
{
    public class CloseInState : EnemyStateBase
    {
        public override void OnDamageTaken()
        {
        }
        
        protected override void Begin()
        {
            Enemy.sprite.color = Color.magenta;
        }
        
        protected override void End()
        {
            Enemy.Rigidbody.velocity = Vector2.zero;
            Enemy.sprite.color = Color.white;
        }
        public override void FixedUpdate()
        {
            if(CloseInCoordinates < 0 )
            {
                Context.pursueMove.DoFixedUpdate(Enemy, -Enemy.TargetDirection);
            } else if(CloseInCoordinates > 1)
            {
                Context.pursueMove.DoFixedUpdate(Enemy, Enemy.TargetDirection);
            } else
            {
                Context.pursueMove.DoFixedUpdate(Enemy, Vector2.zero);
                ExitTo(new EnemyAttackState());
            }
        }
    }
}