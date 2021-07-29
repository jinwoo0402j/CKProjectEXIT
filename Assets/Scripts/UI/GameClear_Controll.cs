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

    public GameObject _T01;

    public GameObject _T02;

    public GameObject _T03;

    public GameObject _T04;

    public Animator Ani_01;

    public Animator Ani_02;

    public Animator Ani_02_1;

    public Animator Ani_03;

    public AudioSource _S01;

    public AudioSource _S02;

    private float Cut_T;

    // Start is called before the first frame update
    void Start()
    {
        Cut_Count = 0;
        Cut_T = 0;
    }

    private void Cut_Control()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Cut_Count++;
            Cut_T = 1.5f;
        }

        switch (Cut_Count)
        {
            case 1:
                _S01.Play();
                _1.SetActive(true);
                break;
            case 2:
                _S01.Stop();
                _2.SetActive(true);
                break;
            case 3:
                _2_1.SetActive(true);
                break;
            case 4:
                _S02.Play();
                _2.SetActive(false);
                _3.SetActive(true);
                break;
            case 5:
                _S02.Stop();
                Ani_01.SetBool("Disa", true);
                Ani_02_1.SetBool("Disa", true);
                Ani_03.SetBool("Disa", true);
                break;
            case 6:
                _1.SetActive(false);
                _2_1.SetActive(false);
                _3.SetActive(false);
                _T01.SetActive(true);
                _T02.SetActive(true);
                break;
            case 7:
                _T03.SetActive(true);
                _T04.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cut_T = Cut_T - Time.deltaTime;
        if (Cut_T <= 0)
        {
            Cut_Control();
        }
    }
}
