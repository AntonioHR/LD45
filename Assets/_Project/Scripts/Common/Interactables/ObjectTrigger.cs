using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class ObjectTrigger<T> : MonoBehaviour
    {
        [SerializeField]
        private bool allowMultipleTriggers = false;

        private bool triggered;

        private void Awake()
        {
            Debug.Assert(gameObject.GetComponentsInChildren<Collider2D>().Any(x=>x.isTrigger));
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!allowMultipleTriggers && triggered)
                return;

            var target = other.GetComponent<T>();

            if(target == null)
            {
                var proxy = other.GetComponent<IProxyFor<T>>();
                if (proxy != null)
                    target = proxy.Owner;
            }

            if(target != null)
            {
                triggered = true;
                OnTriggered(target);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {

            var target = other.GetComponent<T>();

            if(target == null)
            {
                var proxy = other.GetComponent<IProxyFor<T>>();
                if (proxy != null)
                    target = proxy.Owner;
            }

            if(target != null)
            {
                OnExit(target);
            }
        }

        protected virtual void OnExit(T target) { }

        protected abstract void OnTriggered(T obj);

        public void Reset()
        {
            triggered = false;
        }
    }
}
