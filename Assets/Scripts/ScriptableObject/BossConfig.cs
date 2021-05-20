using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;


#if UNITY_EDITOR
using UnityEditor;
[CreateAssetMenu(fileName = "NewBossData", menuName = "BossData", order = 0)]
#endif

[Serializable]
public class BossConfig : ScriptableObject
{
    [Space(20)]


    [SerializeField]
    private int attack_damage;

    [Header("range : 0 - 1")]
    [SerializeField]
    private List<float> phase_rate;

    [SerializeField]
    private int default_hp;

    [Header("unit : second")]
    [SerializeField]
    private float attack_delay;

    [Header("unit : m/s")]
    [SerializeField]
    private float bullet_speed;


    public int ATTACK_DAMAGE { get => attack_damage; }
    public int DEFAULT_HP { get => default_hp; }
    public float ATTACK_DELAY { get => attack_delay; }
    public float BULLET_SPEED { get => bullet_speed; }
    public List<float> PHASE_RATE { get => phase_rate.ToList(); }
}