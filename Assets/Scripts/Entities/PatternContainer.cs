using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

using UnityEngine;
using Utils;

public class PatternContainer : MonoBehaviour
{
    [SerializeField]
    private BasePatternConfig pattern;

    [SerializeField]
    private Rigidbody rigid;

    [SerializeField]
    private TestBulletContainer overrideEntity;


    public BasePatternConfig Pattern { get => pattern; }

    private HitInfo info;
    private TestEntity Origin;
    private TestEntity Destination;
    private float Speed;



    public void Initialize(in HitInfo info, in float speed)
    {
        this.info = info;
        Origin = overrideEntity;
        Destination = info.Destination;
        Speed = speed;
    }

    private void Update()
    {
        rigid.velocity = info.hitDir.normalized * Speed;
    }

    IEnumerator DelayRelease()
    {
        yield return YieldInstructionCache.WaitForSeconds(10f);
        if (gameObject.activeInHierarchy)
        {
            PoolManager.ReleaseObject(gameObject);
        }
    }


    public void Run()
    {
        StartCoroutine(Pattern.Run(this, overrideEntity, Destination));
        StartCoroutine(DelayRelease());
    }
}