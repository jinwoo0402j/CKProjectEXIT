using UnityEngine;
using System;
using System.Collections;

using Utils;


public class TestBulletContainer : TestEntity
{
    public override void TakeDamage(HitInfo info)
    {
        base.TakeDamage(info);
    }

    protected override void Dead()
    {
        base.Dead();
    }
}
