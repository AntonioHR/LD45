using System;
using UnityEngine;
using SamuraiGame.Enemy;

namespace SamuraiGame.Room
{
	[CreateAssetMenu(menuName = "Samurai/WaveConfig")]
	public class WaveConfig : ScriptableObject
	{
        public EnemySpawn[] enemySpawns;
        public int totalEnemies;
		public float startDelay;
        public string triggerEvent;
    }

    [Serializable]
    public class EnemySpawn
    {
        public string SpawnerId;
        public GameObject[] enemyPrefab;
    }
}
