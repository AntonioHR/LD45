using Common.Movement;
using UnityEngine;

namespace SamuraiGame.Player
{
    [CreateAssetMenu(menuName = "Samurai/PlayerConfigs")]
    public class PlayerConfigs : ScriptableObject
    {
        public States.DashState.Configs dash;
        public CharMover defaultMover;
        public CharMover dashMover;
    }
}