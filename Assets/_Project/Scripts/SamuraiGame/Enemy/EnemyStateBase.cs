﻿using UnityEngine;
using System.Collections;
using Common.StateMachines;
using SamuraiGame.Player;
using System;
using SamuraiGame.Enemy.States;

namespace SamuraiGame.Enemy
{
    public abstract class EnemyStateBase : State<EnemyController, EnemyStateBase>
    {
        protected EnemyController Enemy { get => Context; }

        public float SurroundCoordinates{ get => Enemy.Surround.RangeCoordinates(Enemy.target.transform.position); }
        public float CloseInCoordinates{ get => Enemy.CloseIn.RangeCoordinates(Enemy.target.transform.position); }

        public virtual bool TryAttack()
        {
            return false;
        }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void OnStagger() { }

        public abstract void OnDamageTaken();

        public virtual void OnHitParried()
        {
        }
        public virtual void OnPlayerIsInAggroRange(PlayerController player) { }

        public virtual void OnPlayerDead()
        {
            ExitTo(new EnemyIdleState());

        }

        protected void FacePlayer()
        {
            Enemy.FacingDirection = Enemy.TargetDirection;
        }
    }
}
