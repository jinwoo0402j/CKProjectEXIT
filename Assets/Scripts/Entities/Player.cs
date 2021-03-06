using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Utils;

public class Player : TestEntity
{
    [SerializeField]
    private PlayerConfig data;

    [SerializeField]
    private AudioSource Roll_S;

    [SerializeField]
    private AudioSource Hit_S;

    [SerializeField]
    private Rigidbody rb;

    public bool Roll_State;

    public Animator AniCon;

    public float Roll_Sp;

    private float RollSpeed;

    public bool char_state;

    public float Roll_T;

    public bool Roll_State_T;

    public int CoolTime;

    public int CoolTime02;

    public Text Cool_T;

    public bool Boss_D;

    private float god;

    public float GodMode;

    public override float DefaultHP { get => data.DEFAULT_HP; }

    private float Speed { get => data.WALK_SPEED; }

    void Start()
    {
        Roll_T = CoolTime;
        Roll_State_T = true;
        Roll_State = false;
        RollSpeed = 1f;
        AniCon.SetBool("Idle", true);
        AniCon.SetBool("Walk", false);
        HP.CurrentData = DefaultHP;
    }

    private void PlayerMovement()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");

        Vector3 getVel = new Vector3(xMove, 0, zMove).normalized * Speed * RollSpeed;
        rb.velocity = getVel;
        transform.LookAt(transform.position + getVel);

        var Rollbool = AniCon.GetCurrentAnimatorStateInfo(0);

        if (Rollbool.IsName("Rolling") && Rollbool.normalizedTime >= 0.1f && Rollbool.IsName("Rolling") && Rollbool.normalizedTime <= 0.5f)
        {
            RollSpeed = Roll_Sp;
            Roll_State = true;
        }
        else
        {
            RollSpeed = 1f;
            Roll_State = false;;
        }
    }

    private void Roll_Count()
    {
        Roll_T = Roll_T + Time.deltaTime;
       
        if(Roll_T >= CoolTime)
        {
            Roll_State_T = true;
        }
    }

    protected override void Dead()
    {
        base.Dead();
        
    }


    private void Rolling()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Roll_State == false && Roll_State_T == true)
        {
            AniCon.SetBool("Idle", false);
            AniCon.SetBool("Walk", false);
            AniCon.SetBool("Roll", true);
            Roll_S.Play();
            Roll_T = 0;
            Roll_State_T = false;
            god = GodMode;
        }
        else
        {
            AniCon.SetBool("Roll", false);
        }
    }
    private void AnimationControl()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow))
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

    private void CharStat()
    {
        if(base.isDead == true)
        {
            char_state = true;
        }
    }

    private void CoolTime_Text()
    {
        if(CoolTime02 <= 0)
        {
            Cool_T.text = "";
        }
        else
        {
            Cool_T.text = CoolTime02.ToString();
        }
    }

    void Update()
    {
        god = god - Time.deltaTime;
        Boss_D = GameObject.Find("BossDummyResource").GetComponent<TestBoss>()._Dead;
        CoolTime_Text();
        CoolTime02 = CoolTime - (int)Roll_T;
        Roll_Count();
        base.C_Roll = Roll_State;
        base._god_T = god;
        CharStat();
        if (char_state == false && Boss_D == false)
        {
            PlayerMovement();
            AnimationControl();
            Rolling();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Hit_S.Play();
            GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>().VibrateForTime(0.1f);
        }
        else
        {
            GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>().VibrateForTime(0);
        }
    }

}
