using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private CollisionEventRiser eventRiser;

    [SerializeField]
    private Rigidbody rigid;


    private HitInfo info;

    private void Awake()
    {
        eventRiser.OnTriggerEnterEvent += EventRiser_OnTriggerEnterEvent;
    }

    private void EventRiser_OnTriggerEnterEvent(Collider obj)
    {
        if (obj.TryGetComponent<TestEntity>(out var entity))
        {
            if (entity.Type == info.Origin.Type)
                return;

            info.Destination = entity;
            info.hitNormal = (entity.transform.position - transform.position).normalized.ToXZ().ToVector3FromXZ();
            info.hitPoint = transform.position;

            entity.TakeDamage(info);

            PoolManager.ReleaseObject(gameObject);
        }
    }

    public void Initialize(in HitInfo info, in float speed)
    {
        this.info = info;

        rigid.velocity = info.hitDir.normalized * speed;

        StartCoroutine(DelayRelease());
    }

    IEnumerator DelayRelease()
    {
        yield return YieldInstructionCache.WaitForSeconds(10f);
        if (gameObject.activeInHierarchy)
        {
            PoolManager.ReleaseObject(gameObject);
        }
    }

}