using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using Utils;

public class BossHPGauge : MonoBehaviour
{
    [SerializeField]
    private Image preGauge;
    [SerializeField]
    private Image postGauge;

    [SerializeField]
    private AnimationCurve curve;

    private CoroutineWrapper wrapper;

    private BossConfig currentData;

    private void Awake()
    {
        wrapper = new CoroutineWrapper(this);

        TestBoss.OnBossAwaken += TestBoss_OnBossAwaken;
        TestBoss.OnBossRemoved += TestBoss_OnBossRemoved;
    }

    private void TestBoss_OnBossAwaken(TestEntity obj)
    {
        if (obj is TestBoss)
        {
            var boss = obj as TestBoss;
            currentData = boss.Data;
            obj.HP.OnDataChanged += HP_OnDataChanged;
        }

    }

    private void HP_OnDataChanged(float obj)
    {
        var targetRate = obj / currentData.DEFAULT_HP;
        wrapper.StartSingleton(runHPDecrease(targetRate, 0.5f));

        IEnumerator runHPDecrease(float end, float runtime = 1f)
        {
            float t = 0;
            var preStart = preGauge.fillAmount;
            while (t < runtime)
            {
                preGauge.fillAmount = Mathf.Lerp(preStart, end, curve.Evaluate(t / runtime));
                t += Time.unscaledDeltaTime;
                yield return null;
            }
            preGauge.fillAmount = end;

            t = 0;
            var postStart = postGauge.fillAmount;
            while (t < runtime)
            {
                postGauge.fillAmount = Mathf.Lerp(postStart, end, curve.Evaluate(t / runtime));
                t += Time.unscaledDeltaTime * 2;
                yield return null;
            }
            postGauge.fillAmount = end;
        }
    }


    private void TestBoss_OnBossRemoved(TestEntity obj)
    {
        obj.HP.OnDataChanged -= HP_OnDataChanged;
    }


    private void OnDestroy()
    {
        TestBoss.OnBossRemoved -= TestBoss_OnBossRemoved;
        TestBoss.OnBossAwaken -= TestBoss_OnBossAwaken;
    }
}