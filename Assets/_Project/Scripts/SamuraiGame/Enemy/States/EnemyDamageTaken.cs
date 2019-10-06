using UnityEngine;
using System.Collections;

namespace SamuraiGame.Enemy.States
{

    public class EnemyDamageTakenState : EnemyStateBase
    {
        public override void OnDamageTaken()
        {

        }

        public override bool TryAttack()
        {
            ExitTo(new EnemyAttackState());
            return false;
        }

        protected override void Begin()
        {
            RemovePlayerListener();

        }

        public override void OnPlayerDead() { }
    }
}
