using System;
using UnityEngine;

namespace SamuraiGame.Enemy.States
{
    public class PursueState : EnemyStateBase
    {
        public override void OnDamageTaken()
        {
        }
        protected override void Begin()
        {
        }


        public override void Update()
        {
            if(SurroundCoordinates > 0 && SurroundCoordinates < 1)
            {
                ExitTo(new CloseInState());
            }
            
        }
        public override void FixedUpdate()
        {
            FacePlayer();
            if(SurroundCoordinates < 0 )
            {
                Context.pursueMove.DoFixedUpdate(Enemy, -Enemy.TargetDirection);
            } else if(SurroundCoordinates > 1)
            {
                Context.pursueMove.DoFixedUpdate(Enemy, Enemy.TargetDirection);
            } else
            {
                Context.pursueMove.DoFixedUpdate(Enemy, Vector2.zero);
            }
        }
    }
}