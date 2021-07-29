using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameClear_Controll_02 : MonoBehaviour
{
    public int Cut_Count;

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

    private float Cut_T;

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

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("TestPlayerMovement");
        }

        switch (Cut_Count)
        {
            case 1:
                _S01.Play();
                _1.SetActive(true);
                break;
            case 2:
                _2.SetActive(true);
                break;
            case 3:
                _2_1.SetActive(true);
                break;
            case 4:
                _1.SetActive(false);
                _3.SetActive(true);
                break;
            case 5:
                Ani_02.SetBool("Disa", true);
                Ani_02_1.SetBool("Disa", true);
                Ani_03.SetBool("Disa", true);
                break;
            case 6:
                _4.SetActive(true);
                break;
            case 7:
                _5.SetActive(true);
                break;
            case 8:
                _6.SetActive(true);
                break;
            case 9:
                _7.SetActive(true);
                break;
            case 10:
                _8.SetActive(true);
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
