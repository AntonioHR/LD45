using System;
using System.Linq;
using Common.Input;
using Common.Movement;
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


        public CharMover defaultMover { get => configs.defaultMover; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        public Rigidbody2D Rigidbody { get; private set; }
        
        public Vector2 DirectionInput { get => InputHelper.DefaultJoystickInputRawCapped; }
        public Vector2 FacingDirection { get; set; }

        private Health health = new Health(3);

        public int CurrentHealth{ get => health.Current; }


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
            stateMachine.OnHit(source);
            health.Hit();
        }
        public void Heal()
        {
            health.Heal();
        }
        public void OnDead()
        {
            if(Died != null)
                Died();
        }

        public void OnHitByEnemy(bool isHitDashable, Transform enemy)
        {
            if (isHitDashable)
            {
                bool isDashing = stateMachine.IsDashing();
                if (isDashing)
                {
                    return;
                }
            }
            OnHit(enemy);
        }

        public bool IsDashing()
        {
            return stateMachine.IsDashing();
        }
    }
}