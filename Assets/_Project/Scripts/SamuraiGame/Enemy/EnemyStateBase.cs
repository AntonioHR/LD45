using UnityEngine;
using System.Collections;
using Common.StateMachines;
using System;

namespace SamuraiGame.Enemy
{
    public abstract class EnemyStateBase : State<EnemyController, EnemyStateBase>
    {
        protected EnemyController Enemy { get => Context; }

        public virtual bool TryAttack()
        {
            return false;
        }
        public virtual void OnStagger() { }

        public abstract void OnDamageTaken();

        public virtual void OnHitParried()
        {
        }
    }
}
