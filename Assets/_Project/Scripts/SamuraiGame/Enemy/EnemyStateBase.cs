using UnityEngine;
using System.Collections;
using Common.StateMachines;

namespace SamuraiGame.Enemy
{
    public abstract class EnemyStateBase : State<EnemyController, EnemyStateBase>
    {
        protected EnemyController Enemy { get => Context; }

        public virtual bool TryAttack()
        {
            return false;
        }

        public abstract void OnDamageTaken();
    }
}