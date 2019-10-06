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
            return false;
        }

        protected override void Begin()
        {

        }
    }
}
