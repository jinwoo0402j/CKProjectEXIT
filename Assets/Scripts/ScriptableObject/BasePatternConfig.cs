using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using Utils;


#if UNITY_EDITOR
using UnityEditor;
[CreateAssetMenu(fileName = "NewPatternData", menuName = "PatternData", order = 0)]
#endif

[Serializable]
public class BasePatternConfig : ScriptableObject
{
    [Space(20)]

    [SerializeField]
    private GameObject bullet_origin;

    [Header("unit : int")]
    [SerializeField]
    private int bullet_damage;

    [Header("unit : m/s")]
    [SerializeField]
    private float bullet_speed;

    [Header("unit : second")]
    [SerializeField]
    private float pattern_interval;

    [Header("unit : second")]
    [SerializeField]
    private float pattern_runtime;

    [Header("unit : second")]
    [SerializeField]
    private float pattern_delay;

    public GameObject BULLET_ORIGIN { get => bullet_origin; }
    public int BULLET_DAMAGE { get => bullet_damage; }
    public float BULLET_SPEED { get => bullet_speed; }
    public float PATTERN_INTERVAL { get => pattern_interval; }
    public float PATTERN_RUNTIME { get => pattern_runtime; }
    public float PATTERN_DELAY { get => pattern_delay; }

    public virtual IEnumerator Run(TestEntity origin, TestEntity destination)
    {
        float t = 0;
        float runtime = PATTERN_RUNTIME;
        float offset = 0;
        while (t < runtime)
        {
            offset = Mathf.Lerp(0, 360, t / runtime);

            for (int i = 0; i < 8; ++i)
            {
                var dir = Quaternion.Euler(Vector3.up * (Mathf.Lerp(0, 360, ((float)i).Remap((0, 8), (0, 1))) + offset)) * Vector3.forward;

                var instance = PoolManager.SpawnObject(BULLET_ORIGIN);
                instance.transform.position = origin.transform.position + dir.normalized;

                var bullet = CacheManager.Get<Bullet>(instance);
                bullet.Intialize(new HitInfo()
                {
                    Amount = BULLET_DAMAGE,
                    Origin = origin,
                    Destination = destination,
                    hitDir = dir
                }, BULLET_SPEED);
            }

            t += PATTERN_INTERVAL;
            yield return YieldInstructionCache.WaitForSeconds(PATTERN_INTERVAL);
        }

        yield break;
    }
}