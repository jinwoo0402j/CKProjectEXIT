using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Utils;

public class Game_Over : TestEntity
{

    [SerializeField]
    private GameObject Game_Over_T;

    [SerializeField]
    private GameObject Game_Over_I;

    [SerializeField]
    private GameObject Game_Over_B_R;

    [SerializeField]
    private GameObject Game_Over_B_E;

    public bool _char_S;
    public float _time;

    // Start is called before the first frame update
    void Start()
    {
        Game_Over_I.SetActive(false);
        Game_Over_T.SetActive(false);
        Game_Over_B_R.SetActive(false);
        Game_Over_B_E.SetActive(false);
        _char_S = false;
    }

    void CharStat()
    {
        _char_S = GameObject.Find("char").GetComponent<Player>().isDead;
        if (_char_S == true)
        {
            _time = _time + Time.deltaTime;
            Game_Over_I.SetActive(true);
            Game_Over_T.SetActive(true);
            if(_time >= 2)
            {
                Game_Over_B_R.SetActive(true);
                Game_Over_B_E.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CharStat();
    }
}
