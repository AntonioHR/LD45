using UnityEngine;

namespace SamuraiGame.Enemy
{
    [CreateAssetMenu(menuName="Samurai/EnemyConfigs")]
    public class EnemyConfigs : ScriptableObject
    {
        public SurroundRange.Setup surround;
        public SurroundRange.Setup closeIn;

    }
}