
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2 : MonoBehaviour
{

    public float Char_HP;
    public float speed = 100f; // 탄알 이동 속력
    private Rigidbody bulletRigidbody; // 이동에 사용할 리지드 바디 컴포넌트
    // Start is called before the first frame update
    void Start()
    {
        //게임 오브젝트에서 rigidbody 컴포넌트를 찾아 bulletrigidbody에 할당
        bulletRigidbody = GetComponent<Rigidbody>();
        //리지드바디의 속도 = 앞쪽 방향 * 속력
        bulletRigidbody.velocity = transform.forward * speed;
        
        //3초 뒤에 자신의 게임 오브젝트 파괴
        Destroy(gameObject, 6f);
    }

    void OnTriggerEnter(Collider other) {
        //충돌한 상대방 게임 오브젝트가 player 태그를 가진 경우
        if(other.tag == "Player")
        {
            Char_HP = GameObject.Find("char").GetComponent<Player>().HP.CurrentData;
            Char_HP = Char_HP - 1f; 
            GameObject.Find("char").GetComponent<Player>().HP.CurrentData=Char_HP;
            GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>().VibrateForTime(0.1f);
            GameObject.FindWithTag("Hit").GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }
        else
        {
            GameObject.FindWithTag("MainCamera").GetComponent<CameraShake>().VibrateForTime(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}

