using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PuzzleManager : MonoSingleton<PuzzleManager>
{
    public event Action OnPuzzleInvoked;


    public static Notifier<float> OverridedTimeScale { get; private set; } = new Notifier<float>();



    protected override void Awake()
    {
        base.Awake();

        OverridedTimeScale.CurrentData = 1f;

        InvokeInitialized();
    }


    public void SendOpenPuzzle()
    {
        OnPuzzleInvoked?.Invoke();
    }

}
