using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace SamuraiGame.Events
{
    public class CustomTrigger :MonoBehaviour
    {
        public UnityEvent unityEvent;
        public string id;

        bool started;
        public void Start()
        {
            started = true;
            TriggerManager.StartListening(id, unityEvent.Invoke);
        }
        public void OnEnable()
        {
            if(started)
                TriggerManager.StartListening(id, unityEvent.Invoke);
        }
        public void OnDisable()
        {
            TriggerManager.StopListening(id, unityEvent.Invoke);
        }
    }
}
