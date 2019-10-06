using UnityEngine;
using System.Collections.Generic;
using SamuraiGame.Enemy;

namespace SamuraiGame.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        public EnemyManager Instance { get; private set; }

        public GameObject Player;

        private List<EnemyController> toAttackEnemies;

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
    }
}
