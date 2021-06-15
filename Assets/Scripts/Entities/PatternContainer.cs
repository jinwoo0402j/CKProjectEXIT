using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

using UnityEngine;
using Utils;

public class PatternContainer : MonoBehaviour
{
    [SerializeField]
    private BasePatternConfig Pattern;

    [SerializeField]
    private Rigidbody rigid;

    private HitInfo info;
    private TestEntity Origin;
    private TestEntity Destination;
    private float Speed;



    public void Initialize(in HitInfo info, in float speed)
    {
        this.info = info;
        Origin = info.Origin;
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
        StartCoroutine(Pattern.Run(this, Origin, Destination));
        StartCoroutine(DelayRelease());
    }
}