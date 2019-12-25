using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Experimental.Rendering.LWRP;

namespace Common.Utils
{
    public static class TweenExtensions
    {

        public static Tween DOIntensity(this Light2D light, float endValue, float duration)
        {
            return DOTween.To(() => light.intensity, t => light.intensity = t, endValue, duration);
        }
    }
}
