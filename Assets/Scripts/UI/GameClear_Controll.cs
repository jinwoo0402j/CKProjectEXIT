using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear_Controll : MonoBehaviour
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


    // Start is called before the first frame update
    void Start()
    {
        Cut_Count = 0;
        _1.SetActive(false);
        _2.SetActive(false);
        _2_1.SetActive(false);
        _3.SetActive(false);
    }

    private void Cut_Control()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Cut_Count++;
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
                    _3.SetActive(true);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cut_Control();
    }
}
