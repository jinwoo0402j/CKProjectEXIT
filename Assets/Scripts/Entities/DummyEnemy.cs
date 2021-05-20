using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : TestEntity
{
    [SerializeField]
    private int defaultHP = 100;

    private CoroutineWrapper scaleAniWrapper;
    private Vector3 DefaultLocalScale;

    private void Awake()
    {
        base.HP.CurrentData = defaultHP;
        DefaultLocalScale = transform.localScale;
        scaleAniWrapper = new CoroutineWrapper(this);
    }

    public override void TakeDamage(HitInfo info)
    {
        base.TakeDamage(info);
        scaleAniWrapper.StartSingleton(SimpleScaleAni());
    }

    IEnumerator SimpleScaleAni()
    {
        float t = 0;
        transform.localScale = DefaultLocalScale * 1.3f;
        while (t < 1)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, DefaultLocalScale, t);
            yield return null;
        }
        transform.localScale = DefaultLocalScale;
    }
}
