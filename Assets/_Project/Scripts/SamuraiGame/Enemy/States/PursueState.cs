using System;
using Common.Timers;
using UnityEngine;

namespace SamuraiGame.Enemy.States
{
    public class PursueState : EnemyStateBase
    {
        float closeInDelay;
        bool isInrange;

        Stopwatch stopwatch = new Stopwatch();

        public override void OnDamageTaken()
        {
        }
        protected override void Begin()
        {
            closeInDelay = UnityEngine.Random.Range(Enemy.configs.attackDelayMin, Enemy.configs.attackDelayMax);
        }


        public override void Update()
        {
            if(SurroundAttackCoordinates > 0 && SurroundAttackCoordinates < 1)
            {
                if(!isInrange)
                {
                    isInrange = true;
                    stopwatch = Stopwatch.CreateAndStart();
                }
                if(stopwatch.ElapsedSeconds > closeInDelay)
                    ExitTo(new CloseInState());
            } else
            {
                if(isInrange)
                {
                    isInrange = false;

                    stopwatch = new Stopwatch();
                }

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