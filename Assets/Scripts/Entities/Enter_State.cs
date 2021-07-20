using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_State : MonoBehaviour
{
    public bool enter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            enter = true;
        }
    }
    private void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            enter = false;
        }
    }

}
