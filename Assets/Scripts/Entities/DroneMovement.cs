using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Utils;
public class DroneMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private float LerpRate;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private Transform Target;

    void Update()
    {
        
    }

    private void DroneMove()
    {
        var position = InputManager.Instance.MouseWorldXZ.CurrentData;
        var dir = position - transform.position.ToXZ();

        transform.LookAt(transform.position + dir.ToVector3FromXZ());
    }

    void FixedUpdate()
    {
        var targetPosition = new Vector3(Target.position.x + offset.x, offset.y, Target.position.z + offset.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, LerpRate);
    }
}
