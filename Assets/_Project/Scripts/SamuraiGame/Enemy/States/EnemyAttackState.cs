using UnityEngine;
using System.Collections;
using SamuraiGame.Managers;
using SamuraiGame.Player;
using System;

namespace SamuraiGame.Enemy.States {

    public class EnemyAttackState : EnemyStateBase
    {
        private int animationIndex = 0;
        private GameObject currentDamageArea;

        private IEnumerator waitAttackCoroutine;

        protected override void Begin()
        {
            PlayAttackAnimations();
        }

        private void PlayAttackAnimations()
        {
            animationIndex = 0;

            TryPlayNextAnimation();
        }

        private void OnAnimationFinished()
        {
            Debug.Log("startWaiting");
            currentDamageArea?.SetActive(false);
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

            waitAttackCoroutine = Wait(Enemy.attackAnimations[animationIndex].GettingReadyTime, StartNextAnimation);

            Enemy.StartCoroutine(waitAttackCoroutine);
		}

        private void StartNextAnimation()
        {
            Debug.Log("finish waiting");

            string animationId = Enemy.attackAnimations[animationIndex].AnimationId;
            Enemy.animationPlayable.PlayOnce(animationId, OnAnimationFinished);
            currentDamageArea = Enemy.attackAnimations[animationIndex].DamageHitBox;
            currentDamageArea.SetActive(true);
        }

        private void FinishAttack()
        {
            Debug.Log("animationFinished");
            StopAllAnimations();
            ExitTo(new EnemyIdleState());
        }

        private void StopAllAnimations()
        {
            currentDamageArea.SetActive(false);
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

        private void GettingReady(bool isDashable)
        {

        }
    }
}
