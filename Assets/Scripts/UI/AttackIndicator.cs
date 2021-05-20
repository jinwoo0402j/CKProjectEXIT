using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIndicator : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer baseSprite;
    [SerializeField]
    private SpriteRenderer additiveSprite;

    private float Runtime;
    private float Range;


    private CoroutineWrapper wrapper;

    private void Awake()
    {
        wrapper = new CoroutineWrapper(this);
    }

    public void Initialize(in float runtime, in float range)
    {
        Runtime = runtime;
        Range = range;

        baseSprite.transform.localScale = Vector3.one * range;
        additiveSprite.transform.localScale = Vector3.zero;

        wrapper.StartSingleton(Run()).SetOnComplete(OnComplete);
    }

    private IEnumerator Run()
    {
        float t = 0;
        while (t < Runtime)
        {
            additiveSprite.transform.localScale = Vector3.one * Mathf.Lerp(0, Range, t / Runtime);
            t += Time.deltaTime;
            yield return null;
        }

        additiveSprite.transform.localScale = Vector3.one * Range;
    }

    private void OnComplete()
    {
        PoolManager.ReleaseObject(gameObject);
    }

}
