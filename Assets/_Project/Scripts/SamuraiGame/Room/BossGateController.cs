using UnityEngine;
using System.Collections;
using Common.Interactables;
using SamuraiGame.Player;
using SamuraiGame.Events;
using System;
using DG.Tweening;
using System.Threading.Tasks;

namespace SamuraiGame.Room
{
    public class BossGateController : GateController
    {

        public override void Start()
        {
            TriggerManager.StartListening(EventName.OnBossSpawn, OnBossSpawn);
        }

        private async void OnBossSpawn()
        {
            Open();
            await Wait.For(GameConstants.BOSS_WAIT_TIME);

            Close();
        }

        public override void OnDestroy()
        {
            TriggerManager.StopListening(EventName.OnBossSpawn, OnBossSpawn);
        }
    }
}
