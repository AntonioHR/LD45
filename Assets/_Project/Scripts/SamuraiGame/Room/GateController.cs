using UnityEngine;
using System.Collections;
using Common.Interactables;
using Common.Utils;
using SamuraiGame.Player;
using SamuraiGame.Events;
using System;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.Experimental.Rendering.LWRP;

namespace SamuraiGame.Room
{
    public class GateController : ObjectTrigger<PlayerController>
    {
        public float openDistance = 1;
        public float openTime = .5f;
        public float intensityFlicker = .5f;
        public float flickerTime = 1;
        private float targetIntensity;
        [SerializeField]
        private SpriteRenderer left;
        [SerializeField]
        private SpriteRenderer right;
        [SerializeField]
        private Collider2D blocker;
        [SerializeField]
        private Collider2D trigger;
        [SerializeField]
        private UnityEngine.Experimental.Rendering.Universal.Light2D light;

        public void Start()
        {
            TriggerManager.StartListening(EventName.RoomCompleted, Open);
            TriggerManager.StartListening(EventName.OnBossSpawn, OnBossSpawn);
            targetIntensity = light.intensity;
            light.intensity = 0;
        }

        private async void OnBossSpawn()
        {
            Open();
            await Wait.For(GameConstants.BOSS_WAIT_TIME);

            Close();
        }

        public void OnDestroy()
        {
            TriggerManager.StopListening(EventName.OnBossSpawn, OnBossSpawn);
            TriggerManager.StopListening(EventName.RoomCompleted, Open);
        }


        private void Open()
        {
            var seq = DOTween.Sequence();
            seq.Join(left.transform.DOMoveX(-openDistance, openTime).SetRelative());
            seq.Join(right.transform.DOMoveX(openDistance, openTime).SetRelative());
            seq.Join(light.DOIntensity(targetIntensity, openTime));
            seq.OnComplete(OnOpened);
        }

        private void Close()
        {
            var seq = DOTween.Sequence();
            seq.Join(left.transform.DOMoveX(openDistance, openTime).SetRelative());
            seq.Join(right.transform.DOMoveX(-openDistance, openTime).SetRelative());
            seq.OnComplete(OnClosed);
        }

        private void OnOpened()
        {
            blocker.enabled = false;
            trigger.enabled = true;
            light.DOIntensity(light.intensity + intensityFlicker, flickerTime).SetLoops(-1, LoopType.Yoyo);
        }
        private void OnClosed()
        {
            blocker.enabled = true;
            trigger.enabled = false;
        }

        protected override void OnTriggered(PlayerController obj)
        {
            TriggerManager.Trigger(EventName.OnGateEnter);
        }

    }
}
