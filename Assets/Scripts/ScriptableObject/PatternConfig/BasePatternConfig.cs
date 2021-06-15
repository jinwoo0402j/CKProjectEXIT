using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using Utils;


#if UNITY_EDITOR
using UnityEditor;
[CreateAssetMenu(fileName = "[Base]NewPatternData", menuName = "PatternData", order = 0)]
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

    [Header("unit : -1 or 1")]
    [SerializeField]
    private float offset_direction = 1;

    [Header("unit : float")]
    [SerializeField]
    private float offset_speed = 1;

    [Header("unit : 0~360 degree")]
    [SerializeField]
    private float default_offset = 0;

    public GameObject BULLET_ORIGIN { get => bullet_origin; }
    public virtual int BULLET_DAMAGE { get => bullet_damage; }
    public virtual float BULLET_SPEED { get => bullet_speed; }
    public virtual float PATTERN_INTERVAL { get => pattern_interval; }
    public virtual float PATTERN_RUNTIME { get => pattern_runtime; }
    public virtual float PATTERN_DELAY { get => pattern_delay; }
    public virtual float OFFSET_DIRECTION { get => offset_direction; }
    public virtual float OFFSET_SPEED { get => offset_speed; }
    public virtual float DEFAULT_OFFSET { get => default_offset; }

    public virtual IEnumerator Run(MonoBehaviour runner, TestEntity origin, TestEntity destination)
    {
        float t = 0;
        float runtime = PATTERN_RUNTIME;
        float offset = 0;
        while (t < runtime)
        {
            offset = Mathf.Lerp(0, 360, Mathf.Repeat(t * OFFSET_SPEED / runtime, 1));

            for (int i = 0; i < 8; ++i)
            {
                var dir = Quaternion.Euler(Vector3.up * (Mathf.Lerp(0, 360, ((float)i).Remap((0, 8), (0, 1))) + DEFAULT_OFFSET + offset * OFFSET_DIRECTION)) * Vector3.forward;

                var instance = PoolManager.SpawnObject(BULLET_ORIGIN);
                instance.transform.position = origin.transform.position + dir.normalized;

                var bullet = CacheManager.Get<Bullet>(instance);
                bullet.Initialize(new HitInfo()
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