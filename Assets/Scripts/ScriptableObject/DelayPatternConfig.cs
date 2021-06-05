using System.Collections;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using Utils;


#if UNITY_EDITOR
using UnityEditor;
[CreateAssetMenu(fileName = "[Delay]newDelayPatternData", menuName = "DelayPatternData", order = 0)]
#endif

[Serializable]
public class DelayPatternConfig : BasePatternConfig
{
    public override IEnumerator Run(MonoBehaviour runner, TestEntity origin, TestEntity destination)
    {
        yield return YieldInstructionCache.WaitForSeconds(PATTERN_DELAY);
    }
}
