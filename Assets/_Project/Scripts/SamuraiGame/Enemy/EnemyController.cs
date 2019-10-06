using UnityEngine;
using System.Collections;
using Common.Animation;
using System;

namespace SamuraiGame.Enemy
{
    [RequireComponent(typeof(CallbackAnimationPlayer))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private AnimationSetup animationSetup;
        [SerializeField]
        public EnemyAttackAnimation[] attackAnimations;

        [System.NonSerialized]
        public CallbackAnimationPlayer animationPlayable;

        private EnemyStateMachine stateMachine = new EnemyStateMachine();

        internal void HitParried()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            animationPlayable = GetComponent<CallbackAnimationPlayer>();
            animationPlayable.Init(animationSetup);
            
            stateMachine.Begin(this);

            SetAllDamageHitBox();

            TryAttack();
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
                hitBoxComponent.enemy = this;
            }
        }
    }
}
