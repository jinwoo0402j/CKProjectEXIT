using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Utils;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private float LerpRate;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private Transform Target;

    void FixedUpdate()
    {
        var targetPosition = new Vector3(Target.position.x + offset.x, offset.y, Target.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, LerpRate);
    }
}
