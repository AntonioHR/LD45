using UnityEngine;
using System.Collections;

namespace SamuraiGame.Enemy
{
    [System.Serializable]
    public class EnemyAttackAnimation
    {
        [SerializeField]
        private bool isDashable;

        [SerializeField]
        private GameObject damageHitBox;

        [SerializeField]
        private AnimationClip animation;

        public bool IsDashable { get => isDashable; }
        public AnimationClip Animation { get => animation; }
        public GameObject DamageHitBox { get => damageHitBox; }
    }
}
