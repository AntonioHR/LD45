using UnityEngine;
using UInput = UnityEngine.Input;

namespace Common.Input
{
    public static class InputHelper
    {
        public static Vector2 DefaultJoystickInput { get { return new Vector2(UInput.GetAxis("Horizontal"), UInput.GetAxis("Vertical")); } }

        public static Vector2 DefaultJoystickInputRawNotCapped
        {
            get
            {
                var i = new Vector2(UInput.GetAxisRaw("Horizontal"), UInput.GetAxisRaw("Vertical"));
                return i.sqrMagnitude > 1 ? i.normalized : i;
            }
        }
        public static Vector2 DefaultJoystickInputRawCapped {
            get
            {
                var i = new Vector2(UInput.GetAxisRaw("Horizontal"), UInput.GetAxisRaw("Vertical"));
                return i.sqrMagnitude > 1 ? i.normalized : i;
            }
        }
    }
}
