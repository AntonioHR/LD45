using System;
using System.Linq;
using Common.Audio;
using Common.Input;
using Common.Movement;
using SamuraiGame.Events;
using SamuraiGame.Managers;
using UnityEngine;

namespace SamuraiGame.Player
{
    public class PlayerController : MonoBehaviour, IMovingChar
    {
        public event Action<int> HealthChanged{ add => health.ValueChanged+=value; remove => health.ValueChanged -= value; }


        public event Action Died;

        private PlayerStateMachine stateMachine = new PlayerStateMachine();

        public PlayerConfigs configs;
        public Animator animator;
        public ParticleSystem dashParticle;
        public Transform foot;

        public Collider2D dashHitbox;

        public CharMover defaultMover { get => configs.defaultMover; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        public Rigidbody2D Rigidbody { get; private set; }
        
        public Vector2 DirectionInput { get => InputHelper.DefaultJoystickInputRawCapped; }
        public Vector2 FacingDirection { get; set; }

        private Health health = new Health(3);

        public int CurrentHealth{ get => health.Current; }
        public bool CanHeal { get => health.CanHeal; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

            health.Died += OnNoHealth;

        }


        private void Start()
        {
            stateMachine.Begin(this);
        }
        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }
        private void Update()
        {
            UpdateFacingDirection();
            stateMachine.Update();

            if(configs.dashKeys.Any(k=>Input.GetKeyDown(k)))
            {
                stateMachine.OnDashPressed();
            }
        }

        private void UpdateFacingDirection()
        {
            if(DirectionInput.magnitude != 0)
            {
                FacingDirection = DirectionInput.normalized;
            }
        }

        
        private void OnNoHealth()
        {
            stateMachine.OnNoHealth();
        }

        public void OnHit(Transform source)
        {
            animator.SetTrigger("hurt");
            AudioManager.Instance.Play("take_hit");
            stateMachine.OnHit(source);
            health.Hit();
        }
        public void Heal()
        {
            AudioManager.Instance.Play("recover");
            health.Heal();
        }
        public void OnDead()
        {
            TriggerManager.Trigger(EventName.PlayerDeathAnimationEnd);

            if (Died != null)
                Died();
        }

        public void OnHitByEnemy(bool isHitDashable, Transform enemy)
        {
            if (isHitDashable)
            {
                bool isDashing = stateMachine.IsDashing();
                if (isDashing)
                {
                    OnBlock();
                    return;
                }
            }
            OnHit(enemy);
        }

        private void OnBlock()
        {
            AudioManager.Instance.Play("dash_block");
        }

        public bool IsDashing()
        {
            return stateMachine.IsDashing();
        }
        public void OnHitAnimationOver()
        {
            stateMachine.OnHitAnimationOver();
        }
    }
}