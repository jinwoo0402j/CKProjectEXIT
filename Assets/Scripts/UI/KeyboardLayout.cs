using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class KeyboardLayout : MonoBehaviour
{
    [SerializeField]
    private string OverrideCode;
    [SerializeField]
    private KeyCode serializedCode = KeyCode.None;

    public KeyCode code
    {
        get
        {
            if (serializedCode == KeyCode.None && !string.IsNullOrEmpty(OverrideCode))
            {
                if (Enum.TryParse(OverrideCode, out KeyCode value))
                {
                    serializedCode = value;
                }
            }

            return serializedCode;
        }
    }


    public Image image;

}
