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
    private float melee_attack_charge_delay;
    [Header("unit : second")]
    [SerializeField]
    private float melee_attack_delay;

    [Header("unit : int")]
    [SerializeField]
    private int melee_attack_damage;

    [Header("unit : meter")]
    [SerializeField]
    private float melee_attack_threadhold;

    [Header("unit : meter")]
    [SerializeField]
    private float melee_attack_range;

    [Header("unit : m/s")]
    [SerializeField]
    private float bullet_speed;


    public int ATTACK_DAMAGE { get => attack_damage; }
    public int DEFAULT_HP { get => default_hp; }
    public float MELEE_ATTACK_CHARGE_DELAY { get => melee_attack_charge_delay; }
    public float MELEE_ATTACK_DELAY { get => melee_attack_delay; }
    public int MELEE_ATTACK_DAMAGE { get => melee_attack_damage; }
    public float MELEE_ATTACK_THREADHOLD { get => melee_attack_threadhold; }
    public float MELEE_ATTACK_RANGE { get => melee_attack_range; }
    public float BULLET_SPEED { get => bullet_speed; }
    public List<float> PHASE_RATE { get => phase_rate.ToList(); }
}