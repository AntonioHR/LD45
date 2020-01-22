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

        public virtual void Start()
        {
            TriggerManager.StartListening(EventName.RoomCompleted, Open);
        }

        public virtual void OnDestroy()
        {
            TriggerManager.StopListening(EventName.RoomCompleted, Open);
        }


        protected void Open()
        {
            var seq = DOTween.Sequence();
            seq.Join(left.transform.DOMoveX(-openDistance, openTime).SetRelative());
            seq.Join(right.transform.DOMoveX(openDistance, openTime).SetRelative());
            seq.OnComplete(OnOpened);
        }

        protected void Close()
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
