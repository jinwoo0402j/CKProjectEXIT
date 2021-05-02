using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

internal static class YieldInstructionCache
{
    class FloatComparer : IEqualityComparer<float>
    {
        bool IEqualityComparer<float>.Equals(float x, float y)
        {
            return x == y;
        }
        int IEqualityComparer<float>.GetHashCode(float obj)
        {
            return obj.GetHashCode();
        }
    }

    public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();

    private static readonly Dictionary<float, WaitForSeconds> cachedDict = new Dictionary<float, WaitForSeconds>(new FloatComparer());
    private static readonly Dictionary<float, WaitForSecondsRealtime> cachedRealtimeDict = new Dictionary<float, WaitForSecondsRealtime>(new FloatComparer());

    public static WaitForSeconds WaitForSeconds(float seconds)
    {
        if (cachedDict.TryGetValue(seconds, out var value))
            return value;

        value = new WaitForSeconds(seconds);
        cachedDict.Add(seconds, value);
        return value;
    }

    public static WaitForSecondsRealtime WaitForSecondsRealtime(float seconds)
    {
        if (cachedRealtimeDict.TryGetValue(seconds, out var value))
            return value;

        value = new WaitForSecondsRealtime(seconds);
        cachedRealtimeDict.Add(seconds, value);
        return value;
    }
}

public static class CoroutineExtension
{
    /// <param name="onComplete">boolean : complete by condition?</param>
    public static IEnumerator WaitforTimeWhileCondition(this MonoBehaviour runner, float time, Func<bool> condition, Action<bool> onComplete = null)
    {
        bool passed = false;
        bool timeOver = false;
        var timeRoutine = runner.StartCoroutine(WaitForTime(time, () => { passed = true; timeOver = true; }));
        var conditionRoutine = runner.StartCoroutine(WaitWhileCondition(condition, () => passed = true));
        yield return new WaitUntil(() => passed);

        onComplete?.Invoke(!timeOver);

        yield break;
    }

    public static IEnumerator WaitForTime(float time, Action onComplete)
    {
        yield return YieldInstructionCache.WaitForSeconds(time);
        onComplete?.Invoke();
    }
    public static IEnumerator WaitWhileCondition(Func<bool> condition, Action onComplete)
    {
        yield return new WaitWhile(condition);
        onComplete?.Invoke();
    }

    /// <summary>
    /// every update frame, call while runtime
    /// </summary>
    /// <param name="onUpdate">float value is normalized time (0->1)</param>
    /// <returns></returns>
    public static IEnumerator Easy(float runtime, Action<float> onUpdate)
    {
        float t = 0;
        while (t < runtime)
        {
            onUpdate?.Invoke(t / runtime);
            t += Time.deltaTime;
            yield return null;
        }

        onUpdate?.Invoke(1);
    }
}

public class CoroutineWrapper
{
    public static CoroutineWrapper Generate(MonoBehaviour runner) => new CoroutineWrapper(runner);

    public CoroutineWrapper(MonoBehaviour runner) => Runner = runner;



    public Action OnCompleteOnce { get; set; }

    //necessary Data
    public MonoBehaviour Runner { get; private set; }
    public Coroutine Routine { get; private set; }

    private IEnumerator Target;

    public CoroutineWrapper Start(IEnumerator target)
    {
        Target = target;
        Runner.StartCoroutine(RunTarget());

        return this;
    }

    /// <summary>
    /// if routine already running, routine will stop and restart
    /// </summary>
    public CoroutineWrapper StartSingleton(IEnumerator target)
    {
        if (Routine != null)
            Stop();

        return Start(target);
    }

    /// <summary>
    /// When Routine end, call once
    /// </summary>
    public CoroutineWrapper SetOnComplete(Action onComplete)
    {
        OnCompleteOnce += onComplete;

        return this;
    }

    private IEnumerator RunTarget()
    {
        this.Routine = Runner.StartCoroutine(Target);

        yield return Routine;

        Routine = null;

        OnCompleteOnce?.Invoke();
        OnCompleteOnce = null;
    }

    public void Stop()
    {
        if (Routine != null)
        {
            Runner.StopCoroutine(Routine);
            Routine = null;
        }
    }
}