using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using Utils;


#if UNITY_EDITOR
using UnityEditor;
[CreateAssetMenu(fileName = "[ObjectBase]NewPatternData", menuName = "[Object]PatternData", order = 0)]
#endif

[Serializable]
public class ObjectBasePatternConfig : BasePatternConfig
{
    [Space(20)]


    [SerializeField]
    private PatternContainer object_origin;


    public PatternContainer OBJECT_ORIGIN { get => object_origin; }
    public override float PATTERN_RUNTIME => OBJECT_ORIGIN.Pattern.PATTERN_RUNTIME;
    public override float PATTERN_DELAY => OBJECT_ORIGIN.Pattern.PATTERN_DELAY;
    public override float PATTERN_INTERVAL => OBJECT_ORIGIN.Pattern.PATTERN_RUNTIME;

    public override IEnumerator Run(MonoBehaviour runner, TestEntity origin, TestEntity destination)
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

                var instance = PoolManager.SpawnObject(OBJECT_ORIGIN.gameObject);
                instance.transform.position = origin.transform.position + dir.normalized;

                var container = CacheManager.Get<PatternContainer>(instance);

                container.Initialize(new HitInfo()
                {
                    Amount = BULLET_DAMAGE,
                    Origin = origin,
                    Destination = destination,
                    hitDir = dir
                }, BULLET_SPEED);

                container.Run();
            }

            t += PATTERN_INTERVAL;
            yield return YieldInstructionCache.WaitForSeconds(PATTERN_INTERVAL);
        }

        yield break;
    }
}