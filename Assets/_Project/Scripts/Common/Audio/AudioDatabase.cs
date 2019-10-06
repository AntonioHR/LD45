using UnityEngine;

namespace Common.Audio
{
    [CreateAssetMenu(menuName="Common/AudioDatabase")]
    public class AudioDatabase : ScriptableObject
    {
        public SoundEffectAsset[] soundEffects;

    }
}