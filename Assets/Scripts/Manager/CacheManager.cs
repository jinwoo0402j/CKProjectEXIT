using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utils;

public class CacheManager : Singleton<CacheManager>
{

    private class Pair
    {
        public float time;
        public object obj;

        public Pair(float time, object obj)
        {
            this.time = time;
            this.obj = obj;
        }
    }

    private Dictionary<object, Pair> cached = new Dictionary<object, Pair>();

    private int count;

    private int missCount;
    private int hitCount;

    public static T Get<T>(string resourcePath) where T : class
    {
        Instance.cached.TryGetValue(resourcePath, out var pair);
        if (pair == null)
        {
            Instance.missCount++;

            Instance.cached.Add(resourcePath, new Pair(0.0F, Resources.Load(resourcePath)));
            return Get<T>(resourcePath);
        }
        else
        {
            Instance.hitCount++;

            pair.time = Time.time;
            return pair.obj as T;
        }
    }

    public static T Get<T>(Component type) where T : Component
    {
        var gameObject = type.gameObject;

        return Get<T>(gameObject);
    }


    public static T Get<T>(GameObject gameObject) where T : Component
    {
        if (Instance.cached.TryGetValue(gameObject, out var pair))
        {
            Instance.hitCount++;

            Dictionary<Type, object> components = pair.obj as Dictionary<Type, object>;
            if (components.TryGetValue(typeof(T), out var value))
            {
                pair.time = Time.time;
                return value as T;
            }
            else
            {
                if (gameObject.TryGetComponent<T>(out var component))
                {
                    components.Add(typeof(T), component);
                    return component;
                }

                return null;
            }
        }
        else
        {
            Instance.missCount++;

            Instance.cached.Add(gameObject, new Pair(0.0F, new Dictionary<Type, object>()));
            return Get<T>(gameObject);
        }

    }

    [Obsolete]
    public static T[] Gets<T>(GameObject gameObject) where T : Component
    {
        Instance.cached.TryGetValue(gameObject, out var pair);
        if (pair == null)
        {
            Instance.missCount++;

            Instance.cached.Add(gameObject, new Pair(0.0F, new Dictionary<Type, object>()));
            return Gets<T>(gameObject);
        }
        else
        {
            Instance.hitCount++;

            Dictionary<Type, object> components = pair.obj as Dictionary<Type, object>;
            components.TryGetValue(typeof(T[]), out var value);
            if (value == null)
            {
                components.Add(typeof(T[]), gameObject.GetComponents<T>());
                return Gets<T>(gameObject);
            }
            else
            {
                pair.time = Time.time;
                return value as T[];
            }
        }
    }

    public static void UnLoadCacheData(float time)
    {
        int beforeClear = Instance.cached.Count;

        List<object> Keys = new List<object>();

        foreach (KeyValuePair<object, Pair> pair in Instance.cached)
        {
            if (Time.time - pair.Value.time >= time)
            {
                Keys.Add(pair.Key);
            }
        }

        foreach (object obj in Keys)
        {
            Instance.cached.Remove(obj);
        }

        int afterClear = Instance.cached.Count;

        GC.Collect();

        Debug.LogFormat("Cache Cleared : {0} -> {1} ,Total {2} is Removed.", beforeClear, afterClear, beforeClear - afterClear);
        Debug.LogFormat("Cache accuracy: {0} / {1} ", Instance.missCount, Instance.hitCount);

        Instance.missCount = 0;
        Instance.hitCount = 0;
    }
}