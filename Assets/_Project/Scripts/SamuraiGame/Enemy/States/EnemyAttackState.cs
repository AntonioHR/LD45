using UnityEngine;
using System.Collections;

namespace SamuraiGame.Enemy.States {

    public class EnemyAttackState : EnemyStateBase
    {
        private int animationIndex = 0;
        private GameObject currentDamageArea;

        protected override void Begin()
        {
            PlayAttackAnimations();
        }

        

        private void PlayAttackAnimations()
        {
            AnimationClip[] animations = GetAttackAnimationsClips();

            Enemy.animationPlayable.StartPlaying(animations, OnNextAnimation);

            animationIndex = -1;
        }

        private void OnNextAnimation()
        {
            animationIndex++;
            if (animationIndex >= Enemy.attackAnimations.Length)
            {
                FinishAttack();
            }
            else
            {
                Debug.Log("next animation");
                StartNextAnimation();
            }
        }

        private AnimationClip[] GetAttackAnimationsClips()
        {
            AnimationClip[] animations = new AnimationClip[Enemy.attackAnimations.Length];

            for (int i = 0; i < Enemy.attackAnimations.Length; i++)
            {
                animations[i] = Enemy.attackAnimations[i].Animation;
            }
            return animations;
        }

        private void StartNextAnimation()
        {
            currentDamageArea?.SetActive(false);
            currentDamageArea = Enemy.attackAnimations[animationIndex].DamageHitBox;
            currentDamageArea.SetActive(true);
            Debug.Log(currentDamageArea.name);
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
            Enemy.animationPlayable.Stop();
        }

        public override void OnDamageTaken()
        {
            StopAllAnimations();
            ExitTo(new EnemyDamageTakenState());
        }
    }
}
