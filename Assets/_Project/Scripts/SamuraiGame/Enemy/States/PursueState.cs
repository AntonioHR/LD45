using System;
using Common.Timers;
using SamuraiGame.Managers;
using SamuraiGame.Player;
using UnityEngine;

namespace SamuraiGame.Enemy.States
{
    public class PursueState : EnemyStateBase
    {
        float closeInDelay;
        bool isInrange;
        bool isRunning;
        Vector3 lastPosition = Vector3.zero;
        Vector3 preLastPosition = Vector3.zero;

        Stopwatch stopwatch = new Stopwatch();

        protected override void Begin()
        {
            Enemy.animationPlayable.PlayLooped(GameConstants.ENEMY_ANIMATION_RUN, () => { });

            closeInDelay = UnityEngine.Random.Range(Enemy.configs.attackDelayMin, Enemy.configs.attackDelayMax);
            StartRunAnimation();
        }


        private void StartRunAnimation()
        {
            isRunning = true;
            Enemy.animationPlayable.PlayLooped(GameConstants.ENEMY_ANIMATION_RUN, () => { });
        }
        private void StartIdleAnimation()
        {
            isRunning = false;
            Enemy.animationPlayable.PlayLooped(GameConstants.ENEMY_ANIMATION_IDLE, () => { });
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
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            float distance = Vector3.Distance(Enemy.transform.position, lastPosition);
            lastPosition = preLastPosition;
            preLastPosition = Enemy.transform.position;

            if (distance > GameConstants.ENEMY_WALK_ANIMATION_THRESHOLD && !isRunning)
            {
                StartRunAnimation();
            }
            else if (distance <= GameConstants.ENEMY_WALK_ANIMATION_THRESHOLD && isRunning)
            {
                StartIdleAnimation();
            }
        }
    }
}