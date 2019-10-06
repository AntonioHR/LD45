using Common.Audio;
using Common.Interactables;
using DG.Tweening;
using SamuraiGame.Events;
using SamuraiGame.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SamuraiGame
{
    public class GameIntroTrigger : ObjectTrigger<PlayerController>
    {
        public CanvasGroup startOverlay;

        protected override void OnTriggered(PlayerController obj)
        {
            var seq = DOTween.Sequence();
            AudioManager.Instance.Play("intro");
            TriggerManager.Trigger(EventName.OnIntroStarted);

            seq.Append(startOverlay.DOFade(1, .01f));
            seq.AppendInterval(4);
            seq.Append(startOverlay.DOFade(0, .01f));
            seq.AppendCallback(OnFinished);
        }

        private void OnFinished()
        {
            TriggerManager.Trigger(EventName.OnIntroPlayed);
        }
    }
}
