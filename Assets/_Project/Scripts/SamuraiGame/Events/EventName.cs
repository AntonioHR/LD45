using System;

namespace SamuraiGame.Events
{
    public static class EventName
    {
		public static readonly string RoomCompleted = "room-completed";
		public static readonly string OnGateEnter = "on-gate-enter";
        public static readonly string OnTransitionOver = "on-transition-over";
        public static readonly string OnFirstScene= "on-first-scene";
        public static readonly string OnIntroStarted = "on-intro-started";
        public static readonly string OnIntroPlayed = "on-intro-played";
        public static readonly string OnNameShown = "on-name-shown";
        public static readonly string EnemyOutOfCombat = "on-enemy-out-of-combat";
        public static readonly string OnBossSpawn = "on-boss-spawn";
        public static readonly string OnBossKilled = "on-boss-killed";
    }
}
