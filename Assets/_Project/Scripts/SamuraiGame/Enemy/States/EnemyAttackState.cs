using UnityEngine;
using System.Collections;
using SamuraiGame.Managers;
using SamuraiGame.Player;
using System;

namespace SamuraiGame.Enemy.States {

    public class EnemyAttackState : EnemyStateBase
    {
        private int animationIndex = 0;

        private IEnumerator waitAttackCoroutine;
        private bool attacking;

        protected override void Begin()
        {
            Enemy.Rigidbody.velocity = Vector2.zero;
            PlayAttackAnimations();
        }
        protected override void End()
        {
            if(Enemy != null)
                Enemy.Rigidbody.isKinematic = false;
        }

        private void PlayAttackAnimations()
        {
            animationIndex = 0;

            TryPlayNextAnimation();
        }

        private void OnAnimationFinished()
        {
            attacking = false;
            waitAttackCoroutine = Wait(Enemy.attackAnimations[animationIndex].WaitTime, StartNextAttack);

            Enemy.StartCoroutine(waitAttackCoroutine);
        }

        private void StartNextAttack()
        {
            animationIndex++;

            TryPlayNextAnimation();
        }

        private void TryPlayNextAnimation()
        {
            if (animationIndex >= Enemy.attackAnimations.Length)
            {
                FinishAttack();
            }
            else
            {
                StartAttack();
            }
        }

        private void StartAttack()
        {
            float getReadyTime = Enemy.attackAnimations[animationIndex].GettingReadyTime;
            bool isDashable = Enemy.attackAnimations[animationIndex].IsDashable;

            GettingReady(isDashable);

            base.FacePlayer();

            waitAttackCoroutine = Wait(Enemy.attackAnimations[animationIndex].GettingReadyTime, StartNextAnimation);

            Enemy.StartCoroutine(waitAttackCoroutine);
		}

        private void StartNextAnimation()
        {
            UnsetRiposte();

            string animationId = Enemy.attackAnimations[animationIndex].AnimationId;
            attacking = true;
            Enemy.animationPlayable.PlayOnce(animationId, OnAnimationFinished);
            PlayAttackSound();
        }

        private void FinishAttack()
        {
            StopAllAnimations();
            ExitTo(new PursueState());
        }

        private void StopAllAnimations()
        {
            Enemy.StopCoroutine(waitAttackCoroutine);
        }

        public override void OnDamageTaken()
        {
            StopAllAnimations();
            ExitTo(new EnemyDamageTakenState());
        }

        public override void OnStagger()
        {
            StopAllAnimations();
            ExitTo(new EnemyStaggerState());
        }

        public IEnumerator WaitAttackAnimation(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            StartNextAttack();
        }

        private IEnumerator Wait (float seconds, Action callback)
        {
            yield return new WaitForSeconds(seconds);
            callback?.Invoke();
        }

        public override void OnHitParried()
        {
            StopAllAnimations();
            ExitTo(new EnemyStaggerState());
        }

        public override void FixedUpdate()
        {
            Vector2 move = attacking ? Enemy.FacingDirection : Vector2.zero;
            Enemy.configs.hitAdvanceMover.DoFixedUpdate(Enemy, move);

            if(!attacking)
                FacePlayer();
        }

        private void GettingReady(bool isDashable)
        {
            SetRiposteSprite();
        }

        private void SetRiposteSprite()
        {
            string animationId = Enemy.attackAnimations[animationIndex].PrepAnimationId;
            Enemy.animationPlayable.PlayLooped(animationId, ()=> { });
        }

        private void UnsetRiposte()
        {
            if(Enemy.riposteIndicator != null)
            {
                Enemy.riposteIndicator.gameObject.SetActive(false);
            }
        }

        public override void FacePlayer()
        {
            //Enemy.FacingDirection = Enemy.TargetDirection;
        }
    }
}
