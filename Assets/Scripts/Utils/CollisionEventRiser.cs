using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionEventRiser : MonoBehaviour
{
    public event Action<Collider> OnTriggerEnterEvent;
    public event Action<Collider> OnTriggerStayEvent;
    public event Action<Collider> OnTriggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerEnterEvent?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnTriggerExitEvent?.Invoke(other);
    }

    protected void CallOnTriggerStay(Collider other)
    {
        OnTriggerStayEvent?.Invoke(other);
    }
}
