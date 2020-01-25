using Common.Utils;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

namespace SamuraiGame.Util
{
    public class SmoothLightEnabler : MonoBehaviour
    {
        [SerializeField]
        private UnityEngine.Experimental.Rendering.Universal.Light2D light;
        [SerializeField]
        private bool startDisabled = true;
        [SerializeField]
        private float flicker;
        [SerializeField]
        private float lightUpTime = .5f;
        [SerializeField]
        private float flickerTime = 1;
        [SerializeField]
        private AnimationCurve flickerCurve = AnimationCurve.Linear(0, 0, 1, 1);
        private float intensity;
        private Tween tween;

        private void Awake()
        {
            if (light == null)
                light = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();

            intensity = light.intensity;

            if (startDisabled)
                light.intensity = 0;
            else
                StartFlicker();
        }
        public void EnableLight()
        {
            KillCurrentTween();
            tween = light.DOIntensity(intensity, lightUpTime).OnComplete(StartFlicker);
        }

        private void StartFlicker()
        {
            KillCurrentTween();
            tween = light.DOIntensity(intensity + flicker, flickerTime)
                .SetEase(flickerCurve)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void DisableLight()
        {
            KillCurrentTween();
            tween = light.DOIntensity(0, lightUpTime);
        }
        private void OnDestroy()
        {
            KillCurrentTween();
        }

        private void KillCurrentTween()
        {
            if (tween != null)
                tween.Kill();
        }
    }
}
