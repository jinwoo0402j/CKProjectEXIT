using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    [SerializeField]
    private GameObject Boss;

    public float Boss_hp;
    public float Dissolve_C;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Wall_hp();
    }

    void Wall_hp()
    {
        Boss_hp = Boss.GetComponent<TestBoss>().HP.CurrentData;
        Dissolve_C = Boss_hp / 1000 - 0.7f;
        this.GetComponent<Renderer>().material.SetFloat("Vector1_64f45ecfc44240519da5a9970de70f2b", Dissolve_C);
    }
}
