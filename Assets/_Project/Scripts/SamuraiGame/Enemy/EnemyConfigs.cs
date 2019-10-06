using Common.Audio;
using Common.Movement;
using UnityEngine;

namespace SamuraiGame.Enemy
{
    [CreateAssetMenu(menuName="Samurai/EnemyConfigs")]
    public class EnemyConfigs : ScriptableObject
    {
        public CharMover hitAdvanceMover;
        public SurroundRange.Setup surround;
        public SurroundRange.Setup surroundAttack;
        public SurroundRange.Setup closeIn;
        public float attackDelayMin = .25f;
        public float attackDelayMax= 1;
        public Color CloseInColor = Color.red;

        public GameObject healthDrop;
        public float healthDropRate = .3f;
        public SoundEffectAsset[] hitSounds;
    }
}