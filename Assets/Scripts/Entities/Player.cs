using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class Player : TestEntity
{
    [SerializeField]
    private PlayerConfig data;

    [SerializeField]
    private AudioSource Roll_S;

    public Animator AniCon;

    public float RollSpeed;

    public override float DefaultHP { get => data.DEFAULT_HP; }

    private float Speed { get => data.WALK_SPEED; }

    void Start()
    {
        RollSpeed = 1f;
        AniCon.SetBool("Idle", true);
        AniCon.SetBool("Walk", false);
        HP.CurrentData = DefaultHP;
    }

    private void PlayerMovement()
    {
        Vector2 inputAxis = InputManager.Instance.InputRaw.CurrentData;
        transform.Translate(inputAxis.ToVector3FromXZ() * Time.deltaTime * PuzzleManager.OverridedTimeScale.CurrentData * Speed * RollSpeed, Space.World);
        transform.LookAt(transform.position + inputAxis.ToVector3FromXZ());

        var Rollbool = AniCon.GetCurrentAnimatorStateInfo(0);

        if(Rollbool.IsName("Rolling") && Rollbool.normalizedTime >= 0.1f && Rollbool.IsName("Rolling") && Rollbool.normalizedTime <= 0.5f)
        {
            RollSpeed = 3f;
        }
        else 
        {
            RollSpeed = 1f;
        }


    }

    private void Rolling()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AniCon.SetBool("Idle", false);
            AniCon.SetBool("Walk", false);
            AniCon.SetBool("Roll", true);
            Roll_S.Play();
        }
        else
        {
            AniCon.SetBool("Roll", false);
        }
    }
    private void AnimationControl()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
            {
                AniCon.SetBool("Idle", true);
                AniCon.SetBool("Walk", false);
            }
            else
            {
                AniCon.SetBool("Idle", false);
                AniCon.SetBool("Walk", true);
            }
        }
        else
        {
            AniCon.SetBool("Idle", true);
            AniCon.SetBool("Walk", false);
        }

    }




    void Update()
    {
        PlayerMovement();
        AnimationControl();
        Rolling();
    }
}
