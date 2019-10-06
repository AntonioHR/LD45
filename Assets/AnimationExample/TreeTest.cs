using System;
using System.Collections;
using System.Collections.Generic;
using Common.Animation;
using UnityEngine;

public class TreeTest : MonoBehaviour
{
    public bool shouldPlay;
    public bool shouldPlayLoop;

    public string id;
    public CallbackAnimationPlayer p;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldPlay)
        {
            shouldPlay = false;
            p.PlayOnce(id, OnOver);
        } 
        if(shouldPlayLoop)
        {
            shouldPlayLoop = false;
            p.PlayLooped(id, OnLoop);
        }

    }

    private void OnLoop()
    {
        Debug.Log("Loop");
    }

    private void OnOver()
    {
        Debug.Log("Over");

    }
}
