using System;
using UnityEngine;

namespace SamuraiGame.Room
{
	[CreateAssetMenu(menuName = "Samurai/RoomConfig")]
	public class RoomConfig : ScriptableObject
	{
        public WaveConfig[] waves;
    }
}
