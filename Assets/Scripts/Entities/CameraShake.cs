using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public float _Amount;

    float _Shake_T;
    Vector3 CamPosition;

    public void VibrateForTime(float time)
    {
        _Shake_T = time;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CamPosition = this.gameObject.transform.position;

        if (_Shake_T > 0)
        {
            transform.position = Random.insideUnitSphere * _Shake_T + CamPosition;
            _Shake_T -= Time.deltaTime;
        }

        else
        {
            _Shake_T = 0.0f;
            transform.position = CamPosition;
        }
    }
}
