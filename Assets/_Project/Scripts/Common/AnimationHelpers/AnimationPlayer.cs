using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace Common.AnimationHelpers
{
    [RequireComponent(typeof(Animator))]
    public class AnimationPlayer : MonoBehaviour, IAnimationClipSource
    {
        [SerializeField]
        private AnimationClip clip;

        private PlayableGraph playableGraph;
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();

            AnimationPlayableUtilities.PlayClip(animator, clip, out playableGraph);
            //PlayAnimationWordy();
        }

        private void PlayAnimationWordy()
        {
            playableGraph = PlayableGraph.Create($"AnimationPlayer - {gameObject.name}");
            playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

            var playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", animator);
            var clipPlayable = AnimationClipPlayable.Create(playableGraph, clip);
            playableOutput.SetSourcePlayable(clipPlayable);

            playableGraph.Play();
        }

        private void OnDestroy()
        {
            playableGraph.Destroy();
        }

        public void GetAnimationClips(List<AnimationClip> results)
        {
            if(clip != null)
                results.Add(clip);
        }
    }
}
