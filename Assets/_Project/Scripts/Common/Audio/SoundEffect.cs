using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Audio
{
    public class SoundEffect : MonoBehaviour
    {

        [SerializeField]
        private SoundEffectPlayer playerPrefab;


        private SoundEffectAsset asset;
        private List<SoundEffectPlayer> players = new List<SoundEffectPlayer>();

        public void Init(SoundEffectAsset asset)
        {
            this.asset = asset;
            BuildPlayers();
        }

        private void BuildPlayers()
        {
            for (int i = 0; i < asset.playerCount; i++)
            {
                players.Add(BuildPlayer());
            }
        }

        private SoundEffectPlayer BuildPlayer()
        {
            var player = GameObject.Instantiate(playerPrefab, transform);
            player.Init(asset);
            return player;
        }

        public void Play()
        {
            if( LatestPlayTime > asset.minPlayInterval&& HasAvailablePlayers)
            {
                GetAvailablePlayers().First().Play();
            }
        }

        private float LatestPlayTime
        {
            get
            {
                return players.Min(p => p.TimeSinceLastPlay);
            }
        }

        public bool HasAvailablePlayers 
        {
            get
            {
                return GetAvailablePlayers().Any();
            }
        }
        private IEnumerable<SoundEffectPlayer> GetAvailablePlayers()
        {
            return players.Where(p => !p.IsPlaying);
        }
    }
}