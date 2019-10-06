using UnityEngine;
using System.Collections;
using Common.StateMachines;
using SamuraiGame.Enemy.States;
using SamuraiGame.Player;
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

        public void OnPlayerIsInAggroRange(PlayerController player)
        {
            CurrentState.OnPlayerIsInAggroRange(player);
        }

        public void Update()
        {
            CurrentState.Update();
        }
        public void FixedUpdate()
        {
            CurrentState.FixedUpdate();
        }

        public void OnPlayerDead()
        {
            CurrentState.OnPlayerDead();
        }
    }
}
