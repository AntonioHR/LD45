using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace Common.Audio
{
    public class AudioManager : MonoBehaviour
    {
        private Dictionary<SoundEffectAsset, SoundEffect> soundEffects;
        private Dictionary<string, SoundEffect> soundEffectsById;

        [SerializeField]
        private SoundEffect playerPrefab;
        [SerializeField]
        private AudioDatabase audioDatabase;
        [SerializeField]
        private MixerSetup[] mixerSetups;
        [SerializeField]
        private AudioMixer mixer;

        [Serializable]
        public class MixerSetup
        {
            public string Trigger;
            public AudioMixerSnapshot snapshot;
            public float transitionTime = .2f;

            [NonSerialized]
            public AudioMixer mixer;

            public void Start()
            {
                mixer.TransitionToSnapshots(new [] { snapshot}, new[] { 1.0f }, transitionTime);
            }
        }

        public static AudioManager Instance { get; private set; }

        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
                Init();
            } else
            {
                Destroy(this);
            }
        }

        private void Init()
        {
            transform.parent = null;
            DontDestroyOnLoad(this);

            soundEffects = audioDatabase.soundEffects.ToDictionary(a => a, a => BuildAudioPlayer(a));
            soundEffectsById = soundEffects.Where(s => s.Key.HasValidID).ToDictionary(s => s.Key.id, s => s.Value);


            foreach (var mixerSetup in mixerSetups)
            {
                mixerSetup.mixer = mixer;
                TriggerManager.StartListening(mixerSetup.Trigger, mixerSetup.Start);
            }
        }

        private SoundEffect BuildAudioPlayer(SoundEffectAsset asset)
        {
            var player = GameObject.Instantiate(playerPrefab, transform);
            player.name = string.Format("Audio Player [{0}]", asset.name);

            player.Init(asset);
            return player;
        }

        
        public void Play(string id)
        {
            soundEffectsById[id].Play();
        }
        public void Play(SoundEffectAsset soundEffect)
        {
            soundEffects[soundEffect].Play();
        }
    }
}