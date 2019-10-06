using UnityEngine;
using System.Collections;
using Common.StateMachines;
using SamuraiGame.Enemy.States;
using System;

namespace SamuraiGame.Enemy
{
    public class EnemyStateMachine : StateMachine<EnemyController, EnemyStateBase>
    {
        public override EnemyStateBase DefaultState => new EnemyIdleState();

        public bool TryAttack()
        {
            return CurrentState.TryAttack();
        }

        public void OnDamageTaken()
        {
            this.CurrentState.OnDamageTaken();
        }

        public void OnStagger()
        {
            CurrentState.OnStagger();
        }

        public void OnHitParried()
        {
            CurrentState.OnHitParried();
        }
    }
}
