using Common.Movement;
using UnityEngine;

namespace SamuraiGame.Enemy
{
    [CreateAssetMenu(menuName="Samurai/EnemyConfigs")]
    public class EnemyConfigs : ScriptableObject
    {
        public CharMover hitAdvanceMover;
        public SurroundRange.Setup surround;
        public SurroundRange.Setup closeIn;
        public float attackDelayMin = .25f;
        public float attackDelayMax= 1;
    }
}