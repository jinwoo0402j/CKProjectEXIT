using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameClear_Controll_02 : MonoBehaviour
{
    public int Cut_Count;

    private float Cut_T;

    private int Cut_C_Save;

    [SerializeField]
    private GameObject _1;

    [SerializeField]
    private GameObject _2;

    [SerializeField]
    private GameObject _2_1;

    [SerializeField]
    private GameObject _3;

    [SerializeField]
    private GameObject _4;

    [SerializeField]
    private GameObject _5;

    [SerializeField]
    private GameObject _6;

    [SerializeField]
    private GameObject _7;

    [SerializeField]
    private GameObject _8;

    [SerializeField]
    private Animator Ani_01;

    [SerializeField]
    private Animator Ani_02;

    [SerializeField]
    private Animator Ani_02_1;

    [SerializeField]
    private Animator Ani_03;

    [SerializeField]
    private AudioSource _S01;

    [SerializeField]
    private AudioSource _S02;

    [SerializeField]
    private AudioSource _S03;

    [SerializeField]
    private AudioSource _S04;

    [SerializeField]
    private AudioSource _S05;

    [SerializeField]
    private AudioSource _S06;

    [SerializeField]
    private AudioSource _S07;

    [SerializeField]
    private AudioSource _S08;

    [SerializeField]
    private AudioSource _S09;

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
            Cut_C_Save = Cut_Count;
            Cut_T = 1.5f;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("TestPlayerMovement");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        switch (Cut_C_Save)
        {
            case 1:
                _S01.Play();
                _1.SetActive(true);
                Cut_C_Save = 0;
                break;
            case 2:
                _S01.Stop();
                _S02.Play();
                _2.SetActive(true);
                Cut_C_Save = 0;
                break;
            case 3:
                _S09.Play();
                _S02.Stop();
                _2_1.SetActive(true);
                Cut_C_Save = 0;
                break;
            case 4:
                _S09.Stop();
                _S03.Play();
                _1.SetActive(false);
                _3.SetActive(true);
                Cut_C_Save = 0;
                break;
            case 5:
                _S03.Stop();
                Ani_02.SetBool("Disa", true);
                Ani_02_1.SetBool("Disa", true);
                Ani_03.SetBool("Disa", true);
                Cut_C_Save = 0;
                break;
            case 6:
                _S04.Play();
                _4.SetActive(true);
                Cut_C_Save = 0;
                break;
            case 7:
                _S05.Play();
                _S04.Stop();
                _5.SetActive(true);
                Cut_C_Save = 0;
                break;
            case 8:
                _S06.Play();
                _S05.Stop();
                _6.SetActive(true);
                Cut_C_Save = 0;
                break;
            case 9:
                _S07.Play();
                _S06.Stop();
                _7.SetActive(true);
                Cut_C_Save = 0;
                break;
            case 10:
                _S08.Play();
                _S07.Stop();
                _8.SetActive(true);
                Cut_C_Save = 0;
                break;
            case 11:
                SceneManager.LoadScene("TestPlayerMovement");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cut_T = Cut_T - Time.deltaTime;
        if(Cut_T <= 0)
        {
            Cut_Control();
        }
    }
}
