using UnityEngine;
using UnityEngine.UI;

namespace Common.Movement
{

    [RequireComponent(typeof(Text))]
    public class DisplaySpeed : MonoBehaviour
    {
        private Text txt;
        public Rigidbody2D body;

        private void Start()
        {
            txt = GetComponent<Text>();
        }
        private void FixedUpdate()
        {
            txt.text = string.Format("Vel: {0:f2}", body.velocity.magnitude);
        }
    }
}