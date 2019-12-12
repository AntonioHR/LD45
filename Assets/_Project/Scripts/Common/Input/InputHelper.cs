using UnityEngine;
using UInput = UnityEngine.Input;
using VirtualInput =  LeoLuz.PlugAndPlayJoystick.Input;

namespace Common.Input
{
    public static class InputHelper
    {
        public static Vector2 DefaultJoystickInput { get { return new Vector2(UInput.GetAxis("Horizontal"), UInput.GetAxis("Vertical")); } }

        public static Vector2 DefaultJoystickInputRawNotCapped
        {
            get
            {
                var i = new Vector2(VirtualInput.GetAxisRaw("Horizontal"), VirtualInput.GetAxisRaw("Horizontal"));
                return i.sqrMagnitude > 1 ? i.normalized : i;
            }
        }
        public static Vector2 DefaultJoystickInputRawCapped {
            get
            {
                var i = new Vector2(VirtualInput.GetAxisRaw("Horizontal"), VirtualInput.GetAxisRaw("Vertical"));
                return i.sqrMagnitude > 1 ? i.normalized : i;
            }
        }
    }
}
