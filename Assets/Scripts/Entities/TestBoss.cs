using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Utils;

public class TestBoss : TestEntity
{

    public static event Action<TestEntity> OnBossAwaken;
    public static event Action<TestEntity> OnBossRemoved;

    [SerializeField]
    private BossConfig data;
    public BossConfig Data { get => data; }

    [SerializeField]
    private List<BasePatternConfig> patterns;

    [SerializeField]
    private Player player;

    [SerializeField]
    private GameObject _1;

    [SerializeField]
    private GameObject _2;

    [SerializeField]
    private int HP_Under_01;

    [SerializeField]
    private int HP_Under_02;

    [SerializeField]
    private int HP_Under_03;

    private List<Bullet> GeneratedBullets;

    private CoroutineWrapper BarrageRoutine;
    private CoroutineWrapper MeleeAttackRoutine;
    private CoroutineWrapper PhaseRoutine;

    private float LastPatternTime;
    private float LastMeleeAttackTime;
    private BasePatternConfig LastPattern;

    public bool enter;

    public bool _Dead;


    public Notifier<int> Phase { get; private set; } = new Notifier<int>(0);

    private void Awake()
    {
        BarrageRoutine = new CoroutineWrapper(this);
        MeleeAttackRoutine = new CoroutineWrapper(this);
        PhaseRoutine = new CoroutineWrapper(this);

        //dev
        // Phase.OnDataChanged += Phase_OnDataChanged;
    }

    private void Phase_OnDataChanged(int obj)
    {
        Debug.Log("phase Changed : " + obj);
    }

    private void OnEnable()
    {
        StartCoroutine(MainRoutine());
    }

    private void _Dead_Boss()
    {
        if(HP.CurrentData <= 0)
        {
            _Dead = true;
        }
    }

    private void RuntimeInitialize()
    {
        OnBossAwaken?.Invoke(this);

        HP.CurrentData = data.DEFAULT_HP;
    }

    IEnumerator MainRoutine()
    {
        RuntimeInitialize();

        var waitForPuzzle = new WaitUntil(() => PuzzleManager.OverridedTimeScale.CurrentData != 0);

        while (enabled)
        {
            yield return waitForPuzzle;

            if (LastPattern == null || Time.time - LastPatternTime > LastPattern.PATTERN_DELAY)
            {
                LastPatternTime = Time.time;
                LastPattern = patterns.GetRandom();
                BarrageRoutine.StartSingleton(LastPattern.Run(BarrageRoutine.Runner, this, player));
            }

            yield return null;
        }
    }

    IEnumerator MeleeAttack()
    {
        //animation
        yield return YieldInstructionCache.WaitForSeconds(data.MELEE_ATTACK_CHARGE_DELAY);

        if ((player.transform.position - transform.position).magnitude < data.MELEE_ATTACK_RANGE)
        {
            var info = new HitInfo();
            info.Amount = data.MELEE_ATTACK_DAMAGE;
            info.hitPoint = player.transform.position;
            info.Origin = this;
            info.Destination = player;

            player.TakeDamage(info);
        }
    }

    public override void TakeDamage(HitInfo info)
    {
        base.TakeDamage(info);
        Debug.Log("hit : " + info.Amount);

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
        OnBossRemoved?.Invoke(this);
    }  
    private void SetActive()
    {
        if (HP.CurrentData <= HP_Under_01 && HP.CurrentData > HP_Under_02)
        {
            _1.SetActive(true);
        }
        else if(HP.CurrentData <= HP_Under_02 && HP.CurrentData > HP_Under_03)
        {
            _2.SetActive(true);
        }
    }
    private void Start()
    {
        _1.SetActive(false);
        _2.SetActive(false);
    }
    private void Update()
    {
        SetActive();
        _Dead_Boss();
    }


}
