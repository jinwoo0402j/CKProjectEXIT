using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Utils;

public class Game_Over : TestEntity
{

    [SerializeField]
    private GameObject Game_Over_T;

    [SerializeField]
    private GameObject Game_Over_I;

    [SerializeField]
    private GameObject Game_Over_B_E;

    [SerializeField]
    private GameObject spawnner01;

    [SerializeField]
    private GameObject spawnner02;

    [SerializeField]
    private GameObject spawnner03;

    [SerializeField]
    private GameObject Game_Clear_I;

    public bool _Boss_D;
    public bool _char_S;
    public float _time;

    // Start is called before the first frame update
    void Start()
    {
        Game_Over_I.SetActive(false);
        Game_Over_T.SetActive(false);
        Game_Over_B_E.SetActive(false);
        Game_Clear_I.SetActive(false);
        _char_S = false;
    }

    void CharStat()
    {
        _Boss_D = GameObject.Find("BossDummyResource").GetComponent<TestBoss>()._Dead;
        _char_S = GameObject.Find("char").GetComponent<Player>().isDead;
        if (_char_S == true)
        {
            _time = _time + Time.deltaTime;
            Game_Over_I.SetActive(true);
            Game_Over_T.SetActive(true);
            GameObject.Find("BossDummyResource").GetComponent<TestBoss>().enabled = false;
            spawnner01.SetActive(false);
            spawnner02.SetActive(false);
            spawnner03.SetActive(false);

            if (_time >= 2)
            {
                Game_Over_B_E.SetActive(true);
            }
        }
        else if(_Boss_D == true)
        {
            _time = _time + Time.deltaTime;
            Game_Clear_I.SetActive(true);
            GameObject.Find("BossDummyResource").GetComponent<TestBoss>().enabled = false;
            spawnner01.SetActive(false);
            spawnner02.SetActive(false);
            spawnner03.SetActive(false);
            if (_time >= 3)
            {
                SceneManager.LoadScene("GameClear");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CharStat();
    }
}
