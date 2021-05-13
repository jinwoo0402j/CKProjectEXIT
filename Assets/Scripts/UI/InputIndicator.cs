using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class InputIndicator : MonoBehaviour
{
    private class Binder
    {
        public Image Button;
        public Notifier<bool> OnPressNotifier { get; private set; } = new Notifier<bool>();
        public event Action<Image, bool> OnPressed;
        public Predicate<Vector2Int> Predicate;

        public void Initialize(in Image button, in Predicate<Vector2Int> predicate)
        {
            Button = button;
            Predicate = predicate;

            OnPressNotifier.OnDataChanged += OnPressNotifier_OnDataChanged;
            InputManager.OnInitialized += (instance) => instance.InputRaw.OnDataChanged += InputRaw_OnDataChanged;
        }

        private void OnPressNotifier_OnDataChanged(bool isDown)
        {
            OnPressed?.Invoke(this.Button, isDown);
        }

        private void InputRaw_OnDataChanged(Vector2Int obj)
        {
            OnPressNotifier.CurrentData = Predicate(obj);
        }

    }


    [SerializeField]
    private List<KeyboardLayout> serializedTargets;

    private List<Binder> KeyBinders = new List<Binder>();

    [SerializeField]
    private Color DefaultColor;

    [SerializeField]
    private Color PressedColor;

    private void Awake()
    {
        foreach (var target in serializedTargets)
        {
            var binder = new Binder();
            binder.Initialize(target.image, GetPredicateFromKeycode(target.code));
            binder.OnPressed += Binder_OnPressed;

            KeyBinders.Add(binder);
        }
    }

    private void Binder_OnPressed(Image target, bool isDown)
    {
        target.color = isDown ? PressedColor : DefaultColor;
    }

    private Predicate<Vector2Int> GetPredicateFromKeycode(in KeyCode code)
    {
        switch (code)
        {
            case KeyCode.W: return new Predicate<Vector2Int>((target) => target.y > 0);
            case KeyCode.A: return new Predicate<Vector2Int>((target) => target.x < 0);
            case KeyCode.S: return new Predicate<Vector2Int>((target) => target.y < 0);
            case KeyCode.D: return new Predicate<Vector2Int>((target) => target.x > 0);
            default:
                return null;
        }
    }
}
