using Common.AnimationHelpers.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Common.AnimationHelpers
{
    [RequireComponent(typeof(Animator))]
    public class AnimationDynamicQueuePlayable : MonoBehaviour, IAnimationClipSource
    {
        private AnimationClip[] clips = new AnimationClip[0];
        private PlayableGraph playableGraph;
        private PlayQueuePlayable playQueue;

        private bool initialized;

        public void StartPlaying(AnimationClip[] clips, Action OnNewClip)
        {
            playableGraph = PlayableGraph.Create();

            initialized = true;

            var playQueuePlayable = ScriptPlayable<PlayQueuePlayable>.Create(playableGraph);
            playQueue = playQueuePlayable.GetBehaviour();

            playQueue.Initialize(clips, playQueuePlayable, playableGraph, OnNewClip);

            var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());

            playableOutput.SetSourcePlayable(playQueuePlayable);
            playableOutput.SetSourceOutputPort(0);

            playableGraph.Play();
        }

        public void Stop()
        {
            initialized = false;
            playableGraph.Destroy();
        }

        private void OnDestroy()
        {
            if(initialized)
            {
                playableGraph.Destroy();
            }
        }

        void IAnimationClipSource.GetAnimationClips(List<AnimationClip> results)
        {
            foreach (var clip in clips)
            {
                if (clip != null)
                    results.Add(clip);
            }
        }
    }
}
