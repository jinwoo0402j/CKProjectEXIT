using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;


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

    [SerializeField]
    private float pattern_delay;

    public GameObject BULLET_ORIGIN { get => bullet_origin; }
    public float PATTERN_DELAY { get => pattern_delay; }

    public virtual IEnumerator Run() 
    {
        float startTime = Time.time;
        yield break; 
    }
}