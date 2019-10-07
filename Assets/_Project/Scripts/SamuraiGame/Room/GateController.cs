using UnityEngine;
using System.Collections;
using Common.Interactables;
using SamuraiGame.Player;
using SamuraiGame.Events;
using System;
using DG.Tweening;

namespace SamuraiGame.Room
{
    public class GateController : ObjectTrigger<PlayerController>
    {
        public float openDistance = 1;
        public float openTime = .5f;
        [SerializeField]
        private SpriteRenderer left;
        [SerializeField]
        private SpriteRenderer right;
        [SerializeField]
        private Collider2D blocker;
        [SerializeField]
        private Collider2D trigger;

        public void Start()
        {
            TriggerManager.StartListening(EventName.RoomCompleted, Open);
        }
        public void OnDestroy()
        {
            TriggerManager.StopListening(EventName.RoomCompleted, Open);
        }


        private void Open()
        {

            var seq = DOTween.Sequence();
            seq.Join(left.transform.DOMoveX(-openDistance, openTime).SetRelative());
            seq.Join(right.transform.DOMoveX(openDistance, openTime).SetRelative());
            seq.OnComplete(OnOpened);

        }

        private void OnOpened()
        {
            blocker.enabled = false;
            trigger.enabled = true;

        }

        protected override void OnTriggered(PlayerController obj)
        {
            TriggerManager.Trigger(EventName.OnGateEnter);
        }
    }
}
