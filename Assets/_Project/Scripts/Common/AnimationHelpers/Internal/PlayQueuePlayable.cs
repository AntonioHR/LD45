using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Common.AnimationHelpers.Internal
{
    public class PlayQueuePlayable : PlayableBehaviour
    {
        private const int MyPlayableInputPort = 0;
        private const int MixerOutputPort = 0;

        private AnimationMixerPlayable mixer;

        private int currentIndex = -1;
        private float timeToNextClip;

        private Action onNewClip;


        public void Initialize(AnimationClip[] clips, Playable owner, PlayableGraph graph, Action onNewClip = null)
        {
            owner.SetInputCount(1);
            this.onNewClip = onNewClip;

            mixer = AnimationMixerPlayable.Create(graph, clips.Length);

            graph.Connect(mixer, MixerOutputPort, owner, MyPlayableInputPort);
            owner.SetInputWeight(MyPlayableInputPort, 1);

            for (int i = 0; i < clips.Length; i++)
            {
                var clipPlayable = AnimationClipPlayable.Create(graph, clips[i]);
                graph.Connect(clipPlayable, 0, mixer, i);

                mixer.SetInputWeight(i, 1);
            }
        }

        public override void PrepareFrame(Playable playable, FrameData info)
        {

            if (mixer.GetInputCount() == 0)
                return;

            timeToNextClip -= info.deltaTime;

            if (timeToNextClip <= 0)
                MoveToNextClip();

            UpdateWeights();
        }
        private void MoveToNextClip()
        {
            currentIndex++;
            if (currentIndex >= mixer.GetInputCount())
                currentIndex = 0;

            var currentClip = (AnimationClipPlayable)mixer.GetInput(currentIndex);


            currentClip.SetTime(0);
            timeToNextClip = currentClip.GetAnimationClip().length;

            onNewClip?.Invoke();
        }
        private void UpdateWeights()
        {
            for (int clipIndex = 0; clipIndex < mixer.GetInputCount(); ++clipIndex)
            {
                if (clipIndex == currentIndex)
                    mixer.SetInputWeight(clipIndex, 1.0f);
                else
                    mixer.SetInputWeight(clipIndex, 0.0f);
            }
        }
    }
}
