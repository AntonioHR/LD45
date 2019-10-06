using UnityEngine;
using System.Collections;
using Common.StateMachines;
using SamuraiGame.Player;
using System;
using SamuraiGame.Enemy.States;
using SamuraiGame.Managers;

namespace SamuraiGame.Enemy
{
    public abstract class EnemyStateBase : State<EnemyController, EnemyStateBase>
    {
        protected EnemyController Enemy { get => Context; }

        public float SurroundCoordinates{ get => Enemy.target == null? float.PositiveInfinity: Enemy.Surround.RangeCoordinates(Enemy.target.transform.position); }


        public float SurroundAttackCoordinates{ get => Enemy.target == null? float.PositiveInfinity: Enemy.SurroundAttack.RangeCoordinates(Enemy.target.transform.position); }
        public float CloseInCoordinates{ get => Enemy.target == null? float.PositiveInfinity: Enemy.CloseIn.RangeCoordinates(Enemy.target.transform.position); }


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

        protected void RemovePlayerListener()
        {
            Enemy.target.Died -= Enemy.OnPlayerDead;
        }
        protected void TrySpawnPickup()
        {
            if(UnityEngine.Random.value < Context.configs.healthDropRate)
            {
                GameObject.Instantiate(Context.configs.healthDrop, Context.transform.position, Quaternion.identity);
            }
        }

        public virtual void FacePlayer() {
            Enemy.FacingDirection = Enemy.TargetDirection;

            PlayerController player = GameManager.Instance.CurrentScene.Player;

            bool shouldFlip = player.transform.position.x < Enemy.transform.position.x;

            if (shouldFlip && !Enemy.sprite.flipX)
            {
                Enemy.sprite.flipX = true;
            } else if(!shouldFlip && Enemy.sprite.flipX)
            {
                Enemy.sprite.flipX = false;
            }
        }
    }
}
