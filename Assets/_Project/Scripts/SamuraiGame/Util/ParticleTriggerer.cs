using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SamuraiGame.Util
{
    public class ParticleTriggerer : MonoBehaviour
    {
        [Serializable]
        public class Effect
        {
            [SerializeField]
            private ParticleSystem system;
            [SerializeField]
            private ParticleSystem[] extra;
            [SerializeField]
            private int count = 1;
            public string id;

            public void Trigger()
            {
                system.Emit(count);
                foreach (var parts in extra)
                {
                    parts.Emit(count);
                }
            }
        }

        [SerializeField]
        private Effect[] effectSetups;
        private Dictionary<string, Effect> fx;

        private void Start()
        {
            fx = effectSetups.ToDictionary(effect=>effect.id);
        }

        public void FireParticles(string id)
        {
            fx[id].Trigger();
        }
    }
}
