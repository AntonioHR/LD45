using UnityEngine;
using System.Collections;

namespace SamuraiGame.Enemy.States {

    public class EnemyStaggerState : EnemyStateBase
    {
        public override void OnDamageTaken()
        {
            ExitTo(new EnemyDamageTakenState());
        }

        protected override void Begin()
        {
            Debug.Log("staggered");
            //play stagger and run animations
        }
    }
}
