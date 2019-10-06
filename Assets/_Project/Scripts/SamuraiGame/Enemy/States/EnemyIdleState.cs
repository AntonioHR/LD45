using UnityEngine;
using System.Collections;

namespace SamuraiGame.Enemy.States {

    public class EnemyIdleState : EnemyStateBase
    {
        public override void OnDamageTaken()
        {
            ExitTo(new EnemyDamageTakenState());
        }

        public override bool TryAttack()
        {
            ExitTo(new EnemyAttackState());
            return true;
        }

        protected override void Begin()
        {

        }
    }
}
