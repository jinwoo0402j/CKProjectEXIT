using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Utils;

public class TestBoss : TestEntity
{

    [SerializeField]
    private AttackIndicator AttackIndicatorOrigin;

    [SerializeField]
    private BossConfig data;

    [SerializeField]
    private List<BasePatternConfig> patterns;

    private List<Bullet> GeneratedBullets;

    private CoroutineWrapper BarrageRoutine;
    private CoroutineWrapper PhaseRoutine;

    private float LastPatternTime;
    private BasePatternConfig LastPattern;

    public Notifier<int> Phase { get; private set; } = new Notifier<int>(0);

    private void Awake()
    {
        BarrageRoutine = new CoroutineWrapper(this);
        PhaseRoutine = new CoroutineWrapper(this);

        //dev
        Phase.OnDataChanged += Phase_OnDataChanged;
    }

    private void Phase_OnDataChanged(int obj)
    {
        Debug.Log("phase Changed : " + obj);
    }

    private void OnEnable()
    {
        StartCoroutine(MainRoutine());
    }

    IEnumerator MainRoutine()
    {
        var waitForPuzzle = new WaitUntil(() => PuzzleManager.OverridedTimeScale.CurrentData != 0);

        while (enabled)
        {
            yield return waitForPuzzle;

            if (LastPattern == null || Time.time - LastPatternTime > LastPattern.PATTERN_DELAY)
            {
                LastPattern = patterns.GetRandom();
                BarrageRoutine.Start(LastPattern.Run());
            }


            yield return null;
        }
    }



    public override void TakeDamage(HitInfo info)
    {
        base.TakeDamage(info);

        var rate = HP.CurrentData / data.DEFAULT_HP;
        int phase = 0;

        foreach (var r in data.PHASE_RATE)
        {
            if (rate >= r)
                break;

            phase++;
        }

        Phase.CurrentData = phase;
    }

    protected override void Dead()
    {
        //Setting
        BarrageRoutine.Stop();
        PhaseRoutine.Stop();

        //ani

        //release
    }



}
