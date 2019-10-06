using UnityEngine;
using System.Collections;
using Common.Animation;


namespace SamuraiGame.Enemy
{
    [RequireComponent(typeof(CallbackAnimationPlayer))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        public EnemyAttackAnimation[] attackAnimations;

        [System.NonSerialized]
        public CallbackAnimationPlayer animationPlayable;

        private EnemyStateMachine stateMachine = new EnemyStateMachine();

        private void Start()
        {
            stateMachine.Begin(this);
            animationPlayable = GetComponent<CallbackAnimationPlayer>();

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
            }
        }
    }
}
