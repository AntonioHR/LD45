using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SamuraiGame.Enemy;
using SamuraiGame.Room;
using System;

namespace SamuraiGame.Room
{
    [System.Serializable]
    public class Spawner
	{
		public string spawnerId;
		public GameObject spawner;
	}
    [System.Serializable]
	public class RoomSpawner
	{
		[SerializeField]
		private Spawner[] spawners;

        public Vector2 GetSpawnerPosition(string spawnerId)
		{
            foreach(Spawner spawner in spawners)
			{
                if(spawner.spawnerId == spawnerId)
				{
					return spawner.spawner.transform.position;
				}
			}
			throw new System.InvalidOperationException("no spawner with id " + spawnerId);
		}
	}
}