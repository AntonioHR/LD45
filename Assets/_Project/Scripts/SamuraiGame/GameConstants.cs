
using UnityEngine;

namespace SamuraiGame
{
    public static class GameConstants
    {
        public static readonly Color RIPOSTE_UNBLOCKABLE_COLOR = Color.red;
        public static readonly Color RIPOSTE_BLOCKABLE_COLOR = Color.white;

        public static readonly string ENEMY_ANIMATION_DISARM = "disarm";
        public static readonly string ENEMY_ANIMATION_RUN = "run";
        public static readonly string ENEMY_ANIMATION_ESCAPING = "escaping";
        public static readonly string ENEMY_ANIMATION_DYING = "dying";
        public static readonly string ENEMY_ANIMATION_RECOVERING = "recovering";
        public static readonly string ENEMY_ANIMATION_IDLE = "idle";
        public static string ENEMY_ANIMATION_SHEATHE = "recoverying";
        public static float ENEMY_SHEATHE_COOLDOWN = 0.3f;
        public const float ENEMY_WALK_ANIMATION_THRESHOLD = 0.005f;
        public const float BOSS_WAIT_TIME = 5;
        public const float BOSS_WALKING_TIME = 3;
        public const float BOSS_WALKING_Y_DISTANCE = -3;

        public const float BOSS_KILLED_WAIT_TIME = 3;

    }
}
