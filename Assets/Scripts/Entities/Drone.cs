using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class Drone : TestEntity
{
    [SerializeField]
    private PlayerConfig data;

    private float Speed { get => data.WALK_SPEED; }

    private float Damage { get => data.ATTACK_DAMAGE; }

    [SerializeField]
    private float AttackDelay { get => data.ATTACK_DELAY; }

    public bool P_State;

    private float LastAttackTime;

    [SerializeField]
    private float ActionDistance = 300f;


    private float rayLength;


    //[SerializeField]
    //private Animator animator;
    //[SerializeField]
    //private Animator probeAnimator;

    [SerializeField]
    private Animator Sub_Ani;

    [SerializeField]
    private GameObject Marker;

    [SerializeField]
    private Transform MuzzlePosition;


    [SerializeField]
    private Transform ProbeRoot;

    [SerializeField]
    private LineRenderer Bullet;

    [SerializeField]
    private GameObject BulletOrigin;

    [SerializeField]
    private Gradient DefaultBulletGradient;

    [SerializeField]
    private ParticleSystem ProbeMuzzleEffect;

    [SerializeField]
    [Range(0, 1)]
    private float LerpRate;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private Transform Target;

    [SerializeField]
    private AudioSource Laser;

    private CoroutineWrapper probeRotationWrapper;

    private Notifier<bool> OnclickShot = new Notifier<bool>();

    public event Action OnShot;
    private float ClickTime = 0f;

    private NavMeshPath path;

    private CoroutineWrapper BulletWrapper;
    private CoroutineWrapper attackDelayWrapper;
    private CoroutineWrapper markerRoutine;

    public bool Enter_State;

    public bool Boss_D;

    private void Start()
    {
        Bullet.gameObject.SetActive(false);
        path = new NavMeshPath();

        OnclickShot.OnDataChanged += OnclickShot_OnDataChanged;

        BulletWrapper = CoroutineWrapper.Generate(this);
        attackDelayWrapper = CoroutineWrapper.Generate(this);
        markerRoutine = CoroutineWrapper.Generate(this);
        probeRotationWrapper = CoroutineWrapper.Generate(this);

        Sub_Ani.SetBool("Shoot",false);
    }

    protected override void Dead()
    {
        base.Dead();
    }

    private void OnclickShot_OnDataChanged(bool isShot)
    {
        //probeAnimator.SetBool("Click", isShot);

        if (isShot)
        {
            //probeAnimator.SetTrigger("Attack_Ready");
            ClickTime = Time.time;

            probeRotationWrapper.Stop();
        }
        else
        {
            probeRotationWrapper.StartSingleton(resetRotation((22f / 30 + AttackDelay), 0.2f));
            //ProbeRoot.localRotation = Quaternion.Euler(0, 90, 0);

            IEnumerator resetRotation(float firstDelay, float runtime)
            {
                float t = 0;
                yield return YieldInstructionCache.WaitForSeconds(firstDelay);

                Quaternion defaultLotation = ProbeRoot.localRotation;
                while (t < runtime)
                {
                    ProbeRoot.localRotation = Quaternion.Lerp(defaultLotation, Quaternion.Euler(0, 90, 0), t / runtime);
                    t += Time.deltaTime;
                    yield return null;
                }

                ProbeRoot.localRotation = Quaternion.Euler(0, 90, 0);
            }
        }
    }

    private bool TryGetWorldPosition(Vector3 MousePosition, out Vector2 WorldXZPosition)
    {
        WorldXZPosition = Vector2.zero;

        var ray = Camera.main.ScreenPointToRay(MousePosition);

        if (Physics.Raycast(ray, out var hit))
        {
            WorldXZPosition = hit.point.ToXZ();
            return true;
        }

        return false;
    }

    void Update()
    {
        Enter_State = GameObject.Find("Body_Emty").GetComponent<Enter_State>().enter;
        PlayerInput();

        Animation();

        void Animation()
        {
            //if (animator == null)
            //    return;

            //animator.SetBool("Run", Agent.velocity.magnitude > 0.1f);
            //probeAnimator.SetBool("Run", Agent.velocity.magnitude > 0.1f);
        }

        Anicon();
    }

    void FixedUpdate()
    {
        var targetPosition = new Vector3(Target.position.x + offset.x, offset.y, Target.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, LerpRate);
    }

    private void Anicon()
    {
        if(Enter_State == true && P_State == false && Boss_D == false)
        {
            Sub_Ani.SetBool("Shoot", true);
            Sub_Ani.SetBool("Idle", false);
        }
        else
        {
            Sub_Ani.SetBool("Shoot", false);
            Sub_Ani.SetBool("Idle", true);
        }
    }


    private void PlayerInput()
    {
        P_State = Target.GetComponent<Player>().char_state;
        Boss_D = GameObject.Find("BossDummyResource").GetComponent<TestBoss>()._Dead;
        if (Enter_State == true && P_State == false && Boss_D == false)
        {
            OnclickShot.CurrentData = true;

            var position = GameObject.Find("BossDummyResource").transform.position;
            var dir = position - transform.position;
            transform.LookAt(position);
            ProbeRoot.rotation = Quaternion.Euler(Quaternion.LookRotation(dir.normalized).eulerAngles + Quaternion.Euler(0, 90, 0).eulerAngles);


            if (LastAttackTime + AttackDelay < Time.time)
            {
                LastAttackTime = Time.time;

                var hits = Physics.RaycastAll(transform.position.ToXZ().ToVector3FromXZ(), dir.normalized, 100, 1 << LayerMask.NameToLayer("Default"));
                var entities = hits
                    .Select(new Func<RaycastHit, TestEntity>(hit => hit.transform.GetComponent<TestEntity>()))
                    .Where(entity => entity != null && entity.Type == EntityType.Enemy && entity.HP.CurrentData > 0).ToList();

                if (entities != null && entities.Count > 0)
                {
                    var target = entities.First();

                    if (Vector2.Distance(target.transform.position.ToXZ(), transform.position.ToXZ()) > ActionDistance)
                        return;

                    var info = new HitInfo();
                    info.Amount = Damage;
                    info.Origin = this;
                    info.Destination = target;
                    info.hitDir = dir;



                    target.TakeDamageBoss(info);
                    ProbeMuzzleEffect.Play();
                    BulletWrapper.StartSingleton(BulletEffect(0.1f, info));
                    Laser.Play();
                }

                OnShot?.Invoke();
            }


        }
        else
        {
            OnclickShot.CurrentData = false;

        }




        IEnumerator BulletEffect(float moveDelay, HitInfo info)
        {
            Bullet.positionCount = 2;
            Bullet.SetPosition(0, ProbeMuzzleEffect.transform.position);
            Bullet.SetPosition(1, info.Destination.transform.position);

            Bullet.gameObject.SetActive(true);

            float t = 0;
            while (t < moveDelay)
            {
                var gradient = Bullet.colorGradient;
                var keys = DefaultBulletGradient.alphaKeys.Select(new Func<GradientAlphaKey, GradientAlphaKey>((key) => new GradientAlphaKey(1 - (key.alpha * t / moveDelay), key.time))).ToArray();
                gradient.SetKeys(gradient.colorKeys, keys);
                Bullet.colorGradient = gradient;
                t += Time.deltaTime;
                yield return null;
            }

            Bullet.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        OnclickShot.OnDataChanged -= OnclickShot_OnDataChanged;
    }
}