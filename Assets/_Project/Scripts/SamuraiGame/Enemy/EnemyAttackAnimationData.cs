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
        private string animationId;

        [SerializeField]
        private float waitTime;

        public bool IsDashable { get => isDashable; }
        public string AnimationId { get => animationId; }
        public GameObject DamageHitBox { get => damageHitBox; }
        public float WaitTime { get => waitTime; }
    }
}
