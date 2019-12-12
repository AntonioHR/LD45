using UnityEngine;

namespace Common.Utils
{
    public class MobileOnly : MonoBehaviour
    {
        public bool allowInEditor = false;

        public bool IsValidPlatform { get=>Application.isMobilePlatform || (allowInEditor && Application.isEditor); }

        private void Start()
        {
            
            if(!IsValidPlatform)
            {
                gameObject.SetActive(false);
            }
        }
        
    }
}