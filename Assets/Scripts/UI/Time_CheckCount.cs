using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_CheckCount : MonoBehaviour
{
    // Start is called before the first frame update

    public int Time_Count;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time_Count = (int)Time.time;
    }
}
