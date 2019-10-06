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


        public void Start()
        {
            TriggerManager.StartListening(id, unityEvent.Invoke);
        }
        public void DontDestroy()
        {
            TriggerManager.StopListening(id, unityEvent.Invoke);
        }
    }
}
