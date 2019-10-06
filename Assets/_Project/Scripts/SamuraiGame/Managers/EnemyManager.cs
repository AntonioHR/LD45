﻿using UnityEngine;
using System.Collections.Generic;
using SamuraiGame.Enemy;
using SamuraiGame.Room;
using SamuraiGame.Events;
using System.Threading.Tasks;

namespace SamuraiGame.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance { get; private set; }

        [SerializeField]
        private RoomConfig currentRoomConfig;

        [SerializeField]
        private RoomSpawner roomSpawner;

        private int currentWaveIndex;

        private int numberOfEnemiesAlive;


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

        private class EnemyWithPosition
        {
            public Vector2 spawnPosition;
            public GameObject enemy;
        }

        private void Start()
        {
            //StartRoom(currentRoomConfig);
        }

        public async Task StartRoom(RoomConfig roomConfig)
        {
            currentWaveIndex = 0;
            await SpawnWave(roomConfig.waves[0]);
        }

        public async Task SpawnWave(WaveConfig spawnConfig)
        {
			await Wait.For(spawnConfig.startDelay);

            List<List<EnemyWithPosition>> enemies = CreateSpawnEnemiesStructure(spawnConfig);

            numberOfEnemiesAlive = 0;
            int totalEnemies = GetWaveTotalEnemies(spawnConfig.totalEnemies, enemies);

            for(int i = 0; i < totalEnemies; i ++)
            {
                EnemyWithPosition enemy = GetRandomEnemyWithSpawnPoint(enemies);

                GameObject enemyObject = Instantiate(enemy.enemy, enemy.spawnPosition, Quaternion.identity);

                EnemyController controller = enemyObject.GetComponent<EnemyController>();

                KeepTrackOfWave(controller);
            }
        }

        private int GetWaveTotalEnemies(int totalEnemies, List<List<EnemyWithPosition>> enemies)
        {
            int totalEnemiesOnWave = 0;
            foreach(List<EnemyWithPosition>wave in enemies)
            {
                totalEnemiesOnWave += wave.Count;
            }

            if(totalEnemies <= 0)
            {
                return totalEnemiesOnWave;
            } else
            {
                return System.Math.Min(totalEnemies, totalEnemiesOnWave);
            }
        }

        private List<List<EnemyWithPosition>> CreateSpawnEnemiesStructure(WaveConfig spawnConfig)
        {
            List<List<EnemyWithPosition>> enemies = new List<List<EnemyWithPosition>>();
            foreach (EnemySpawn spawn in spawnConfig.enemySpawns)
            {
                List<EnemyWithPosition> enemiesSpawn = new List<EnemyWithPosition>();
                foreach (GameObject enemyPrefab in spawn.enemyPrefab)
                {
                    string spawnerId = spawn.SpawnerId;
                    Vector2 spawnPosition = roomSpawner.GetSpawnerPosition(spawnerId);

                    EnemyWithPosition enemy = new EnemyWithPosition
                    {
                        enemy = enemyPrefab,
                        spawnPosition = spawnPosition
                    };
                    enemiesSpawn.Add(enemy);
                }
                enemies.Add(enemiesSpawn);
            }
            return enemies;
        }

        private EnemyWithPosition GetRandomEnemyWithSpawnPoint(List<List<EnemyWithPosition>> enemies)
        {
            int randomSpawnerIndex = Random.Range(0, enemies.Count);

            List<EnemyWithPosition> spawnerEnemies = enemies[randomSpawnerIndex];

            int randomEnemyIndex = Random.Range(0, spawnerEnemies.Count);

            EnemyWithPosition enemy = spawnerEnemies[randomEnemyIndex];

            spawnerEnemies.RemoveAt(randomEnemyIndex);

            if(spawnerEnemies.Count == 0)
            {
                enemies.RemoveAt(randomSpawnerIndex);
            }

            return enemy;
        }

        private void KeepTrackOfWave(EnemyController enemy)
        {
            numberOfEnemiesAlive++;

            enemy.RegisterOnEnemyOutOfCombat(OnEnemyOutOfCombat);
        }

        private void OnEnemyOutOfCombat()
        {
            numberOfEnemiesAlive--;

            if(numberOfEnemiesAlive <= 0)
            {
                TryStartNextWave();
            }
        }

        private void TryStartNextWave()
        {
            currentWaveIndex++;
            if (currentWaveIndex >= currentRoomConfig.waves.Length)
            {
                TriggerManager.Trigger(EventName.RoomCompleted);
            } else
            {
                WaveConfig nextWave = currentRoomConfig.waves[currentWaveIndex];
                _ = SpawnWave(nextWave);
            }
        }
    }
}
