using UnityEngine;
using System.Collections;
using SamuraiGame.Events;

public class EndingCutscene : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        TriggerManager.Trigger(EventName.OnEndingCutscen);
    }

}
