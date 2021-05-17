using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
[CreateAssetMenu(fileName = "NewPuzzleData", menuName = "PuzzleData", order = 0)]
#endif

[Serializable]
public class PuzzleConfig : ScriptableObject
{
    [SerializeField]
    private PuzzleSceneType type;

    [SerializeField]
    private SceneAsset target_scene;

    public SceneAsset TARGET_SCENE { get => target_scene; }

    public PuzzleSceneType TYPE { get => type; }
}

public enum PuzzleSceneType
{
    None,

}