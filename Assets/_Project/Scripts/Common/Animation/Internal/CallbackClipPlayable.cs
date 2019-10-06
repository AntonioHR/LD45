using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Common.Animation.Internal
{
    public class CallbackClipPlayable : PlayableBehaviour
    {
        private const int MyPlayableInputPort = 0;
        private const int MixerOutputPort = 0;

        private AnimationMixerPlayable mixer;

        private int currentIndex = -1;
        private float timeToNextClip;

        private enum Mode { Looping, Once };

        private Mode mode;

        private Action onClipOver;


        Dictionary<string, int> clipIndices = new Dictionary<string, int>();


        #region Initialization
        public void Initialize(AnimationSetup.Entry[] entries, Playable owner, PlayableGraph graph, string defaultClip = null)
        {
            owner.SetInputCount(1);

            mixer = AnimationMixerPlayable.Create(graph, entries.Length);

            graph.Connect(mixer, MixerOutputPort, owner, MyPlayableInputPort);
            owner.SetInputWeight(MyPlayableInputPort, 1);

            for (int i = 0; i < entries.Length; i++)
            {
                clipIndices.Add(entries[i].id, i);
                
                
                var clipPlayable = AnimationClipPlayable.Create(graph, entries[i].clip);
                // clipPlayable.Pause();
                graph.Connect(clipPlayable, 0, mixer, i);

                mixer.SetInputWeight(i, 1);
            }
        }
        #endregion

        #region Playable Implementation
        public override void PrepareFrame(Playable playable, FrameData info)
        {

            if (mixer.GetInputCount() == 0)
                return;
            if (currentIndex != -1)
            {
                timeToNextClip -= info.deltaTime;

                if (timeToNextClip <= 0)
                    OnClipOver();
            }

            UpdateWeights();
        }

        private void OnClipOver()
        {
            switch(mode)
            {
                case Mode.Looping:
                    onClipOver?.Invoke();
                    ChangeClipTo(currentIndex);
                    break;
                case Mode.Once:
                    GetCurrentClip().Pause();
                    onClipOver?.Invoke();
                    onClipOver = null;
                    break;
                default:
                    throw new NotImplementedException();
            }
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
        #endregion


        public void PlayOnce(string id, Action callback)
        {
            mode = Mode.Once;
            StopCurrent();
            int indx = clipIndices[id];
            ChangeClipTo(indx);
            onClipOver = callback;
        }

        public void PlayLooped(string id, Action loopCallback)
        {
            mode = Mode.Looping;
            StopCurrent();

            ChangeClipTo(clipIndices[id]);
            onClipOver = loopCallback;
        }
        
        private void StopCurrent()
        {
            if(currentIndex != -1)
                GetCurrentClip().Pause();
        }
        private void ChangeClipTo(int indx)
        {
            currentIndex = indx;
            var clip = GetCurrentClip();
            timeToNextClip = (float)clip.GetAnimationClip().length;
            clip.SetTime(0);
            clip.Play();
        }

        

        private AnimationClipPlayable GetCurrentClip()
        {
            return (AnimationClipPlayable)mixer.GetInput(currentIndex);
        }

        
    }
}