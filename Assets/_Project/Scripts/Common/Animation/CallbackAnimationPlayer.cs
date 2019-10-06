using System;
using System.Collections.Generic;
using Common.Animation.Internal;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Common.Animation
{
    [RequireComponent(typeof(Animator))]
    public class CallbackAnimationPlayer : MonoBehaviour, IAnimationClipSource
    {
        [SerializeField]
        private bool autoInit;
        [SerializeField]
        private AnimationSetup setup;


        private PlayableGraph playableGraph;


        private CallbackClipPlayable clipPlayable;
        bool hasInit = false;

        public void Start()
        {
            if(autoInit)
                Init(setup);

        }
        
        public void Init(AnimationSetup setup)
        {
            if(hasInit)
                Cleanup();
            playableGraph = PlayableGraph.Create();

            var playable = ScriptPlayable<CallbackClipPlayable>.Create(playableGraph);
            clipPlayable = playable.GetBehaviour();
            clipPlayable.Initialize(setup.entries, playable, playableGraph);

            var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", GetComponent<Animator>());

            playableOutput.SetSourcePlayable(playable);
            playableOutput.SetSourceOutputPort(0);

            playableGraph.Play();
            hasInit = true;
        }
        
        public void PlayOnce(string id, Action callback)
        {
            clipPlayable.PlayOnce(id, callback);
        }
        public void PlayLooped(string id, Action callback)
        {
            clipPlayable.PlayLooped(id, callback);
        }

        private void OnDestroy()
        {
            Cleanup();
        }
        private void Cleanup()
        {
            playableGraph.Destroy();
            hasInit = false;
        }

        void IAnimationClipSource.GetAnimationClips(List<AnimationClip> results)
        {
            foreach (var e in setup.entries)
            {
                if (e.clip != null)
                    results.Add(e.clip);
            }
        }
    }
}