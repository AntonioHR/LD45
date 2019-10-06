using UnityEngine;
using System.Collections;
using Common.Interactables;
using SamuraiGame.Player;
using SamuraiGame.Events;

namespace SamuraiGame.Room
{
    public class GateController : ObjectTrigger<PlayerController>
    {
        protected override void OnTriggered(PlayerController obj)
        {
            TriggerManager.Trigger(EventName.OnGateEnter);
        }
    }
}
