using UnityEngine;
using UnityEngine.Audio;

namespace Common.Audio
{
    [CreateAssetMenu(menuName="Audio/SoundEffect")]
    public class SoundEffectAsset : ScriptableObject
    {
        public bool HasValidID{ get => id.Length > 0; }
        public string id;
        public int playerCount = 2;
        public float minPlayInterval = .1f;
        [Range(0, 1)]
        public float volume = 1;
        public AudioClip clip;
        public AudioMixerGroup output;
    }
    public static class SoundEffectAssetExtensions
    {
        public static void Play(this SoundEffectAsset soundEffect)
        {
            AudioManager.Instance.Play(soundEffect);
        }
    }
}