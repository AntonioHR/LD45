using UnityEngine;
using System.Collections;
using Common.StateMachines;
using SamuraiGame.Enemy.States;

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
    }
}
