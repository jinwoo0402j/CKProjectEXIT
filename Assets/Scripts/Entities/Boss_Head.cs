using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Head : MonoBehaviour
{

    [SerializeField]
    public GameObject Char;
    public GameObject Dummy;
    public Animator Boss;

    public float rotate_x;
    public float D_rotate;
    public float R_Speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rotate_x = this.transform.localRotation.eulerAngles.x;
        Rotation();
    }

    private void Rotation()
    {
        Vector3 dir = Char.transform.position - Dummy.transform.position;
        Vector3 dir2 = Char.transform.position;
        Vector3 dir3 = new Vector3(dir2.x ,this.transform.position.y ,dir2.z);
        if (rotate_x > 324 || rotate_x < 36)
        {
            transform.LookAt(dir3);
            Boss.SetBool("S_Walk", false);
            Boss.SetBool("Idle", true);
        }
        else
        {
            transform.LookAt(dir3);
            Dummy.transform.rotation = Quaternion.Lerp(Dummy.transform.rotation, Quaternion.LookRotation(dir), R_Speed * Time.deltaTime);
            Boss.SetBool("S_Walk", true);
            Boss.SetBool("Idle", false);
        }

    }
}
