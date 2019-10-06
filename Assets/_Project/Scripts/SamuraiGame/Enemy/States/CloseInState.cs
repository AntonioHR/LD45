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
            Enemy.animationPlayable.PlayLooped(GameConstants.ENEMY_ANIMATION_RUN, () => { });
            Enemy.sprite.color = Enemy.configs.CloseInColor;
        }
        
        protected override void End()
        {
            Enemy.Rigidbody.velocity = Vector2.zero;
            Enemy.sprite.color = Color.white;
        }
        public override void FixedUpdate()
        {
            FacePlayer();

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