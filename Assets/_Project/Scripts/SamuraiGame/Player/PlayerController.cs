using System;
using System.Collections.Generic;
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
        public ParticleSystem deathParticle;
        public ParticleSystem[] dashParticles;
        public ParticleSystem[] blockParticles;
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

        private bool disableInteractions;
        private bool invulnerable;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

            health.Died += OnNoHealth;

        }


        private void Start()
        {
            stateMachine.Begin(this);
            TriggerManager.StartListening(EventName.OnBossSpawn, OnBossSpawn);
        }

        private void OnDestroy()
        {
            TriggerManager.StopListening(EventName.OnBossSpawn, OnBossSpawn);
        }

        private void DisableInsteractions()
        {
            disableInteractions = true;
            //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        private async void OnBossSpawn()
        {
            disableInteractions = true;
            await Wait.For(GameConstants.BOSS_WAIT_TIME);
            disableInteractions = false;
        }

        private void FixedUpdate()
        {
            if(disableInteractions)
            {
                defaultMover.DoFixedUpdate(this, -GetComponent<Rigidbody2D>().velocity);
                return;
            }
            stateMachine.FixedUpdate();
        }
        private void Update()
        {
            if(disableInteractions)
            {
                return;
            }
            UpdateFacingDirection();
            stateMachine.Update();

            if(HasDashed())
            {
                stateMachine.OnDashPressed();
            }
        }

        private bool HasDashed()
        {
            return HasDashedJoystick() || HasDashedKeyboard() || HasDashedVirtualButton();
        }

        private bool HasDashedVirtualButton()
        {
            return LeoLuz.PlugAndPlayJoystick.Input.GetButtonDown("Submit");
        }

        private bool HasDashedJoystick()
        {
            string baseJoystickKey = "joystick button ";
            int size = 4;

            for(int i = 0; i < size; i ++)
            {
                string key = baseJoystickKey + i;
                bool hasPressed = Input.GetKey(key);
                if(hasPressed)
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasDashedKeyboard()
        {
            if (configs.dashKeys.Any(k => Input.GetKeyDown(k)))
            {
                return true;
            }
            return false;
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
            if(invulnerable) {
                return;
            }
            animator.SetTrigger("hurt");
            AudioManager.Instance.Play("take_hit");
            stateMachine.OnHit(source);
            health.Hit();

            SetInvulnerable();
        }

        private async void SetInvulnerable()
        {
            try {
                invulnerable = true;
                animator.SetBool("invulnerability", true);

                await Wait.For(GameConstants.INVULNERABLE_TIME);

                if(gameObject == null) {
                    return;
                }

                animator.SetBool("invulnerability", false);
                invulnerable = false;
            } catch(Exception e) {
                Debug.Log(e);
            }
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
            animator.SetTrigger("blocked");
            foreach(ParticleSystem ps in blockParticles)
            {
                ps.Emit(1);
            }
        }

        public bool IsDashing()
        {
            return stateMachine.IsDashing();
        }
        public void OnHitAnimationOver()
        {
            stateMachine.OnHitAnimationOver();
        }
        public void OnDeathKeyframe()
        {
            AudioManager.Instance.Play("player_death_keyframe");
            deathParticle.Emit(1);
        }
    }
}