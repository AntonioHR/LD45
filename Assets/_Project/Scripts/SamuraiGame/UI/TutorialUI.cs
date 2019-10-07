using UnityEngine;
using System.Collections;
using SamuraiGame.Events;
using DG.Tweening;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    private bool executed;
    public Text text;
    // Use this for initialization
    void Start()
    {
        TriggerManager.StartListening(EventName.EnemyOutOfCombat, FadeOut);
    }

    public void FadeOut()
    {
        if(executed)
        {
            return;
        }
        executed = true;

        var invi = new Color(1, 1, 1, 0);
        var seq = DOTween.Sequence();
        seq.Append(text.DOColor(Color.white, .05f));
        seq.Append(text.DOColor(invi, .05f));
        seq.AppendInterval(.1f);
        seq.SetLoops(6);
    }
}
