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
        private float openDistance;
        [SerializeField]
        private float openTime;

        public void Start()
        {


            TriggerManager.StartListening(EventName.OnGateEnter, Close);

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
        }

        private void OnClosed()
        {
            TriggerManager.Trigger(EventName.OnTransitionOver);
        }

        private void Open()
        {
            var seq = DOTween.Sequence();
            seq.Join(left.transform.DOMoveX(-openDistance, openTime).SetRelative());
            seq.Join(right.transform.DOMoveX(openDistance, openTime).SetRelative());
        }
    }
}
