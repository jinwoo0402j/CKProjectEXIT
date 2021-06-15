using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.UI;

using UnityEngine;
using Utils;

public class PlayerHPGauge : MonoBehaviour
{
    [SerializeField]
    private TestEntity entity;


    [SerializeField]
    private Image preGauge;
    [SerializeField]
    private Image postGauge;

    [SerializeField]
    private AnimationCurve curve;

    private CoroutineWrapper wrapper;

    private float DefaultHP;

    private void Awake()
    {
        wrapper = new CoroutineWrapper(this);
        DefaultHP = entity.DefaultHP;
        entity.HP.OnDataChanged += HP_OnDataChanged;
    }

    private void HP_OnDataChanged(float obj)
    {
        var targetRate = obj / DefaultHP;
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


    private void OnDestroy()
    {
        entity.HP.OnDataChanged += HP_OnDataChanged;
    }


}