using UnityEngine;
using System;
using System.Collections;

using Utils;

public class HitInfo
{
    public Vector3 hitPoint;
    public Vector3 hitNormal;
    public Vector3 hitDir;

    public TestEntity Origin;
    public TestEntity Destination;

    public float Amount;

}


public class TestEntity : MonoBehaviour
{
    public enum EntityType
    {
        None,
        Player,
        Enemy,
    }

    [SerializeField]
    protected EntityType MyType;
    public EntityType Type { get => MyType; }
    public virtual float DefaultHP { get; }
    public virtual bool CharState { get; }

    public Notifier<float> HP = new Notifier<float>();

    public event Action<HitInfo> OnHit;
    public event Action<TestEntity> OnDead;

    public bool isDead = false;

    public bool C_Roll;

    public float _god_T;

    public TestEntity()
    {
        HP.OnDataChanged += HP_OnDataChanged;
    }

    private void HP_OnDataChanged(float obj)
    {
        if (isDead && obj > 0)
        {
            isDead = false;
        }
    }

    public virtual void TakeDamage(HitInfo info)
    {
        if (_god_T < 0)
        {
            HP.CurrentData -= info.Amount;
            OnHit?.Invoke(info);

            if (HP.CurrentData <= 0)
            {
                if (isDead)
                    return;

                isDead = true;

                Dead();
                OnDead?.Invoke(this);
            }
            else
            {
                isDead = false;
            }

        }
    }
    public virtual void TakeDamageBoss(HitInfo info)
    {
        HP.CurrentData -= info.Amount;
        OnHit?.Invoke(info);

        if (HP.CurrentData <= 0)
        {
            if (isDead)
                return;

            isDead = true;

            Dead();
            OnDead?.Invoke(this);
        }
        else
        {
            isDead = false;
        }
    }


    protected virtual void Dead()
    {
        
    }
}
