﻿using UnityEngine;
using System.Collections;
using Common.Animation;
using System;
using SamuraiGame.Enemy.Triggers;
using SamuraiGame.Player;
using Common.Movement;

namespace SamuraiGame.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CallbackAnimationPlayer))]
    public class EnemyController : MonoBehaviour, IMovingChar
    {
        [SerializeField]
        private AnimationSetup animationSetup;
        [SerializeField]
        public EnemyAttackAnimation[] attackAnimations;
        [SerializeField]
        private PlayerTrigger aggroTrigger;
        
        public SpriteRenderer sprite;
        public SpriteRenderer emoteSprite;

        
        public CharMover pursueMove;
        public EnemyConfigs configs;

        [System.NonSerialized]
        public CallbackAnimationPlayer animationPlayable;

        private EnemyStateMachine stateMachine = new EnemyStateMachine();
        internal PlayerController target;

        public Vector2 TargetDirection {
            get
            {
                return ToTarget.normalized;
            }
        }
        public Vector2 ToTarget
        {
            get{
                if(target == null)
                    return Vector2.zero;
                else
                    return (target.transform.position - transform.position);

            }
        }

        public Rigidbody2D Rigidbody { get; private set; }

        public Vector2 FacingDirection { get; set; }
        public bool IsTooCloseToTarget { get; internal set; }

        public SurroundRange CloseIn;
        public SurroundRange Surround;

        private void Awake()
        {
            SetupTriggers();
            Rigidbody = GetComponent<Rigidbody2D>();
            CloseIn = new SurroundRange(transform, configs.closeIn);
            Surround = new SurroundRange(transform, configs.surround);
        }

        private void SetupTriggers()
        {
            aggroTrigger.OnHasPlayer += stateMachine.OnPlayerIsInAggroRange;
        }

        internal void HitParried()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            animationPlayable = GetComponent<CallbackAnimationPlayer>();
            animationPlayable.Init(animationSetup);
            
            stateMachine.Begin(this);

            SetAllDamageHitBox();

        }
        private void Update()
        {
            stateMachine.Update();
        }
        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }

        public bool TryAttack()
        {
            return stateMachine.TryAttack();
        }
        
        public void OnPlayerDead()
        {
            stateMachine.OnPlayerDead();
        }


        private void SetAllDamageHitBox()
        {
            foreach(EnemyAttackAnimation attack in attackAnimations)
            {
                bool isDashable = attack.IsDashable;
                GameObject hitBox = attack.DamageHitBox;

                hitBox.AddComponent<DamageHitBox>();
                DamageHitBox hitBoxComponent = hitBox.GetComponent<DamageHitBox>();
                hitBoxComponent.isDashable = isDashable;
                hitBoxComponent.enemy = this;
            }
        }
    }
}
