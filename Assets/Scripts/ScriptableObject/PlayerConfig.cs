using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
[CreateAssetMenu(fileName = "NewPlayerData", menuName = "PlayerData", order = 0)]
#endif

[Serializable]
public class PlayerConfig : ScriptableObject
{
    [Space(20)]
    

    [SerializeField]
    private int attack_damage;

    [SerializeField]
    private int default_hp;

    [Header("unit : second")]
    [SerializeField]
    private float attack_delay;

    [Header("unit : m/s")]
    [SerializeField]
    private float bullet_speed;

    [Header("unit : m/s")]
    [SerializeField]
    private float walk_speed;

    public int ATTACK_DAMAGE { get => attack_damage; }
    public int DEFAULT_HP { get => default_hp; }
    public float ATTACK_DELAY { get => attack_delay; }
    public float BULLET_SPEED { get => bullet_speed; }
    public float WALK_SPEED { get => walk_speed; }
}