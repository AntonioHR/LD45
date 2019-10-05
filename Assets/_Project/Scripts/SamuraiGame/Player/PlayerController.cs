using Common.Input;
using Common.Movement;
using UnityEngine;

namespace SamuraiGame.Player
{
    public class PlayerController : MonoBehaviour, IMovingChar
    {
        private PlayerStateMachine stateMachine = new PlayerStateMachine();

        public PlayerConfigs configs;

        public CharMover defaultMover { get => configs.defaultMover; }
        public CharMover dashMover { get => configs.dashMover; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        public Rigidbody2D Rigidbody { get; private set; }
        
        public Vector2 DirectionInput { get => InputHelper.DefaultJoystickInputRawCapped; }
        public Vector2 FacingDirection { get; set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();

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
            stateMachine.Update();

            if(Input.GetKeyDown(KeyCode.Z))
            {
                stateMachine.OnDashPressed();
            }
        }
    }
}