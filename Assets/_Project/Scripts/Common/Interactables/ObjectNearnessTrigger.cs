using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Common.Interactables
{
    [RequireComponent(typeof(Collider))]
    public abstract class ObjectNearnessTrigger<T> : MonoBehaviour
        where T: MonoBehaviour
    {
        private List<T> currentObjs = new List<T>();
        public IEnumerable<T> CurrentObjs{get{return currentObjs.AsReadOnly();}}

        [SerializeField]
        public float minRange = 0;
        [SerializeField]
        public float maxRange = 1;


        private void Awake()
        {
            Debug.Assert(gameObject.GetComponentsInChildren<Collider>().Any(x => x.isTrigger));
        }

        private void OnTriggerEnter(Collider other)
        {
            T target = TryGetObjectFrom(other);

            if (target != null)
            {
                currentObjs.Add(target);
                if(currentObjs.Count == 1)
                {
                    OnHasObjects();
                }
            }
        }


        private void OnTriggerExit(Collider other)
        {
            T target = TryGetObjectFrom(other);

            if (target != null)
            {
                currentObjs.Remove(target);
                if (currentObjs.Count == 0)
                {
                    OnHasNoObjects();
                }
            }
        }

        private void LateUpdate()
        {
            foreach (var nearbyObj in currentObjs)
            {
                var dist = Vector3.Distance(transform.position, nearbyObj.transform.position);
                float lerpedNearness = 1 - Mathf.InverseLerp(minRange, maxRange, dist);
                UpdateNearness(nearbyObj, lerpedNearness);
            }
            AfterNearnessUpdate();
        }


        protected abstract void OnHasObjects();
        protected abstract void OnHasNoObjects();
        protected abstract void UpdateNearness(T player, float lerpedNearness);
        protected virtual void AfterNearnessUpdate(){}


        private static T TryGetObjectFrom(Collider other)
        {
            var target = other.GetComponent<T>();

            if (target == null)
            {
                var proxy = other.GetComponent<IProxyFor<T>>();
                if (proxy != null)
                    target = proxy.Owner;
            }

            return target;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, minRange);
            Gizmos.DrawWireSphere(transform.position, maxRange);
        }

        public void SetMinRange(float min)
        {
            minRange = min;
        }

        public void SetMaxRange(float max)
        {
            maxRange= max;
        }


    }
}
