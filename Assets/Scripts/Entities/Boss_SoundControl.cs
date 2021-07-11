using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SoundControl : MonoBehaviour
{

    public Animator AniCon;
    public AudioSource Idle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Sound_C()
    {
        var Idlebool = AniCon.GetCurrentAnimatorStateInfo(0);

        if (Idlebool.IsName("Idle") == true)
        {
            Idle.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Sound_C();
    }
}
