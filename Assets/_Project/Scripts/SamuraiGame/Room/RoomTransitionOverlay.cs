using Common.Audio;
using DG.Tweening;
using SamuraiGame.Events;
using SamuraiGame.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace SamuraiGame.Room
{
    public class RoomTransitionOverlay : MonoBehaviour
    {
        [SerializeField]
        private Image left;
        [SerializeField]
        private Image right;
        [SerializeField]
        private Image announce;

        [SerializeField]
        private float openDistance;
        [SerializeField]
        private float openTime;
        private bool dead;

        public void Start()
        {


            TriggerManager.StartListening(EventName.OnGateEnter, Close);
            TriggerManager.StartListening(EventName.PlayerDeathAnimationEnd, CloseOnDeath);

            if (!GameManager.Instance.CurrentScene.IsFirstScene)
            {
                left.enabled = true;
                right.enabled = true;
            }
            Open();
        }

        private void OnDestroy()
        {
            TriggerManager.StopListening(EventName.OnGateEnter, Close);
            TriggerManager.StopListening(EventName.PlayerDeathAnimationEnd, CloseOnDeath);
        }

        private void CloseOnDeath()
        {
            dead = true;
            Close();
        }

        private void Close()
        {
            left.enabled = true;
            right.enabled = true;
            var seq = DOTween.Sequence();
            seq.Join(left.transform.DOMoveX(openDistance, openTime).SetRelative());
            seq.Join(right.transform.DOMoveX(-openDistance, openTime).SetRelative());
            seq.SetEase(Ease.OutBounce);
            seq.AppendInterval(.75f);
            seq.OnComplete(OnClosed);

            AudioManager.Instance.Play("transition_start");
        }

        private void OnClosed()
        {
            if(!dead)
                TriggerManager.Trigger(EventName.OnTransitionOver);
            else
                TriggerManager.Trigger(EventName.OnTransitionOverDead);
        }

        private void Open()
        {
            AudioManager.Instance.Play("transition_end");
            var seq = DOTween.Sequence();
            seq.Join(left.transform.DOMoveX(-openDistance, openTime).SetRelative());
            seq.Join(right.transform.DOMoveX(openDistance, openTime).SetRelative());

            if (announce != null)
            {
                seq.AppendInterval(2f);
                var size = announce.rectTransform.sizeDelta;
                size.x = 0;
                seq.Append(announce.rectTransform.DOSizeDelta(size, 1f).SetEase(Ease.OutExpo));

                seq.AppendCallback(OnNameShown);
                //seq.AppendCallback(() => announce.gameObject.SetActive(false));
            }
        }

        private void OnNameShown()
        {
            TriggerManager.Trigger(EventName.OnNameShown);
        }
    }
}
