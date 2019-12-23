using UnityEngine;
using System.Collections;
using Common.Animation;
using System;
using SamuraiGame.Enemy.Triggers;
using SamuraiGame.Player;
using Common.Movement;
using SamuraiGame.Managers;
using DG.Tweening;
using Pathfinding;

namespace SamuraiGame.Enemy
{
    [RequireComponent(typeof(AIPath))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CallbackAnimationPlayer))]
    public class EnemyController : MonoBehaviour, IMovingChar
    {
        [SerializeField]
        public AnimationSetup animationSetup;
        [SerializeField]
        public EnemyAttackAnimation[] attackAnimations;
        [SerializeField]
        private PlayerTrigger aggroTrigger;
        
        public SpriteRenderer sprite;
        public SpriteRenderer emoteSprite;

        public CharMover pursueMove;
        public EnemyConfigs configs;

        public ParticleSystem swordParticles;
        public ParticleSystem lightAttackTellParticles;

        //Child objects that need to be flipped when the sprite is flipped
        public Transform flippables;

        [System.NonSerialized]
        public CallbackAnimationPlayer animationPlayable;

        public SpriteRenderer riposteIndicator;

        private EnemyStateMachine stateMachine = new EnemyStateMachine();
        internal PlayerController target;

        private event Action onEnemyOutOfCombat;

        private AIPath aIPath;
        private AIDestinationSetter destinationSetter;

        [SerializeField]
        public bool isBoss;

        public Vector2 TargetDirection {
            get
            {
                return ToTarget.normalized;
            }
        }
        public Vector2 ToTarget
        {
            get{
                SetTarget();
                return aIPath.desiredVelocity;
            }
        }

        private void SetTarget()
        {
            if(destinationSetter.target == null && target != null) {
                destinationSetter.target = target.transform;
            }
        }

        public Rigidbody2D Rigidbody { get; private set; }

        public Vector2 FacingDirection { get; set; }
        public bool IsTooCloseToTarget { get; internal set; }

        public Sprite idleSprite;

        public SurroundRange CloseIn;
        public SurroundRange Surround;

        public SurroundRange SurroundAttack;

        public bool disableInteractions;

        private void Awake()
        {
            SetupTriggers();
            Rigidbody = GetComponent<Rigidbody2D>();
            aIPath = GetComponent<AIPath>();
            destinationSetter = GetComponent<AIDestinationSetter>();

            CloseIn = new SurroundRange(transform, configs.closeIn);
            Surround = new SurroundRange(transform, configs.surround);
            SurroundAttack = new SurroundRange(transform, configs.surroundAttack);
        }
        private void OnDestroy()
        {
            DOTween.Kill(this);
        }

        private void SetupTriggers()
        {
            aggroTrigger.OnHasPlayer += stateMachine.OnPlayerIsInAggroRange;
        }

        internal void HitParried()
        {
            stateMachine.OnStagger();
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
            if(disableInteractions) {
                return;
            }
            stateMachine.Update();

            FacePlayer();
        }

        private void FacePlayer()
        {
            stateMachine.FacePlayer();
        }

        private void FixedUpdate()
        {
            if(disableInteractions) {
                return;
            }
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

                hitBox.AddComponent<EnemyAttackHitbox>();
                EnemyAttackHitbox hitBoxComponent = hitBox.GetComponent<EnemyAttackHitbox>();
                hitBoxComponent.isHitbox = isDashable;
                hitBoxComponent.enemy = this;
            }
        }

        public void RegisterOnEnemyOutOfCombat(Action deathAction)
        {
            onEnemyOutOfCombat += deathAction;
        }
        public void RemoveOnEnemyOutOfCombat(Action deathAction)
        {
            onEnemyOutOfCombat -= deathAction;
        }
        public void TriggerOnOutOfCombat()
        {
            onEnemyOutOfCombat?.Invoke();
        }

        private void LateUpdate()
        {
            stateMachine.LateUpdate();
        }
    }
}
