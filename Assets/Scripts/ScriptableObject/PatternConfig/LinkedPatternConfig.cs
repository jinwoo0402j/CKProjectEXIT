using System.Collections;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using Utils;


#if UNITY_EDITOR
using UnityEditor;
[CreateAssetMenu(fileName = "[Linked]NewLinkedPatternData", menuName = "LinkedPatternData", order = 0)]
#endif

[Serializable]
public class LinkedPatternConfig : BasePatternConfig
{
    [Header("unit : PatternData")]
    [SerializeField]
    private List<BasePatternConfig> patterns;

    public List<BasePatternConfig> PATTERNS { get => patterns; }

    public override float PATTERN_DELAY => patterns.Sum(pattern => pattern.PATTERN_DELAY);
    public override float PATTERN_RUNTIME => patterns.Sum(pattern => pattern.PATTERN_RUNTIME);

    public override IEnumerator Run(MonoBehaviour runner, TestEntity origin, TestEntity destination)
    {
        foreach(var pattern in PATTERNS)
        {
            yield return runner.StartCoroutine(pattern.Run(runner, origin, destination));
        }
    }
}
