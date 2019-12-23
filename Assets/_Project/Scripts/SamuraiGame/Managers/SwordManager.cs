
using UnityEngine;

namespace SamuraiGame.Managers
{
    public class SwordManager : MonoBehaviour
    {
        public static SwordManager Instance { get; private set; }

        [SerializeField]
        private Transform swordLayer;

        [SerializeField]
        private GameObject swordFallPrefab;

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            } else
            {
                Destroy(this);
            }
        }

        public void AnimateSword(Vector2 position) {
            GameObject sword = Instantiate(swordFallPrefab, position, Quaternion.identity);
        }
    }

}