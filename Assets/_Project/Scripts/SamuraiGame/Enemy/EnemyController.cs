using UnityEngine;
using System.Collections;
using Common.AnimationHelpers;
using System;

namespace SamuraiGame.Enemy
{
    [RequireComponent(typeof(AnimationDynamicQueuePlayable))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        public EnemyAttackAnimation[] attackAnimations;

        [System.NonSerialized]
        public AnimationDynamicQueuePlayable animationPlayable;

        private EnemyStateMachine stateMachine = new EnemyStateMachine();

        private void Start()
        {
            stateMachine.Begin(this);
            animationPlayable = GetComponent<AnimationDynamicQueuePlayable>();

            SetAllDamageHitBox();

            TryAttack();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        public void HitParried()
        {
            Destroy(this);
        }

        public bool TryAttack()
        {
            return stateMachine.TryAttack();
        }

        private void SetAllDamageHitBox()
        {
            foreach(EnemyAttackAnimation attack in attackAnimations)
            {
                bool isDashable = attack.IsDashable;
                GameObject hitBox = attack.DamageHitBox;

                hitBox.AddComponent<DamageHitBox>();
                DamageHitBox hitBoxComponent = hitBox.GetComponent<DamageHitBox>();
                hitBoxComponent.isDashable = isDashable;
            }
        }
    }
}
