using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utils;


public class TestCoroutineWrapper : MonoBehaviour
{


    private CoroutineWrapper wrapper;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        wrapper = new CoroutineWrapper(this);

        wrapper.Start(routine()).OnCompleteOnce += () =>
        {
            Debug.Log("Finish");
        };

        yield return new WaitForSeconds(0.5f);
        wrapper.Stop();
    }

    IEnumerator routine()
    {
        yield return new WaitForSeconds(1f);
    }

}
