using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class TestSortArm : MonoBehaviour
{
    [Serializable]
    public class contrainer
    {
        public List<Transform> list;
        public Transform this[int index]
        {
            get
            {
                return list[index];
            }
            set
            {
                list[index] = value;
            }
        }

        public int Count => list.Count;
    }

    [SerializeField]
    private List<contrainer> sub;

    private void OnEnable()
    {
        int count = sub.Count;
        int innerCount = sub.First().Count;

        for (int i = 0; i < innerCount; ++i)
        {
            for (int j = count - 1; j > 0; --j)
            {
                sub[j][i].SetParent(sub[j - 1][i], true);
            }
        }
    }
}
