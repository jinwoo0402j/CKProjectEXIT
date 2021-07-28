using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear_Controll : MonoBehaviour
{
    public int Cut_Count;

    public GameObject _1;

    public GameObject _2;

    public GameObject _2_1;

    public GameObject _3;

    public Animator Ani_01;

    public Animator Ani_02;

    public Animator Ani_02_1;

    public Animator Ani_03;


    // Start is called before the first frame update
    void Start()
    {
        Cut_Count = 0;
    }

    private void Cut_Control()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Cut_Count++;
        }

        switch (Cut_Count)
        {
            case 1:
                _1.SetActive(true);
                break;
            case 2:
                _2.SetActive(true);
                break;
            case 3:
                _2_1.SetActive(true);
                break;
            case 4:
                _2.SetActive(false);
                _3.SetActive(true);
                break;
            case 5:
                Ani_01.SetBool("Disa", true);
                Ani_02_1.SetBool("Disa", true);
                Ani_03.SetBool("Disa", true);
                break;
            case 6:
                _1.SetActive(false);
                _2_1.SetActive(false);
                _3.SetActive(false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cut_Control();
    }
}
