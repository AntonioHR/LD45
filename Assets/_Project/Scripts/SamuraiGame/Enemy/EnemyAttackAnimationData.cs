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

        [SerializeField]
        private float gettingReadyTime;

        [SerializeField]
        private string prepAnimationId;
        [SerializeField]
        private string prepFxId;

        public bool IsDashable { get => isDashable; }
        public string AnimationId { get => animationId; }
        public GameObject DamageHitBox { get => damageHitBox; }
        public float WaitTime { get => waitTime; }
        public float GettingReadyTime { get => gettingReadyTime; }
        public string PrepAnimationId { get => prepAnimationId; }
        public string PrepFxId { get => prepFxId; }
    }
}
