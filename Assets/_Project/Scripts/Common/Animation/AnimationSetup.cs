using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.Animation
{
    [CreateAssetMenu(menuName="Common/AnimationSetup")]
    public class AnimationSetup : ScriptableObject
    {
        [Serializable]
        public class Entry
        {
            public string id;
            public AnimationClip clip;
        }

        public Entry[] entries;

        public Dictionary<string, AnimationClip> BuildDict()
        {
            return entries.ToDictionary(c => c.id, c => c.clip);
        }
    }
}