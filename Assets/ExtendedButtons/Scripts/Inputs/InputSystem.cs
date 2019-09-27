﻿using UnityEngine;
using System;

namespace ExtendedButtons.CustomInput
{
    /// <summary>
    /// Helpful to write once code for all inputs
    /// </summary>
    public class InputSystem : MonoBehaviour
    {
        private static event Action OnInputChange;
        private static ButtonsListener buttonsListener;
        public static bool Enabled = true;

        private static Input _input;
        public static Input Input
        {
            get {
                return _input;
            }
            set {
                if (_input == null || _input.GetType() != value.GetType())
                {
                    _input = value;
                    buttonsListener = new ButtonsListenerCustomInput(_input);
                    OnInputChange?.Invoke();
                }
            }
        }

        /// <summary>
        /// Force to switch input even if is the same type as last input
        /// for example helpful to change to the same input but with another constructor
        /// </summary>
        /// <param name="value"></param>
        public static void ForceChangeInput(Input value)
        {
            _input = value;
            buttonsListener = new ButtonsListenerCustomInput(_input);
            OnInputChange?.Invoke();
        }

        public void Awake()
        {

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
            _input = new MouseInput();
#elif UNITY_ANDROID || UNITY_IPHONE
            _input = new TouchInput();
#endif
            buttonsListener = new ButtonsListenerCustomInput(_input);
        }

        public void Update()
        {
            if (!Enabled) return; 
            Input?.Tick();
            buttonsListener?.Listener();
        }

        public bool WasButtonPressed(int id)
        {
            if (InputEnable())
                return Input.WasButtonPressed(id);
            return false;
        }
        
        public bool IsButtonPressDown(int id)
        {
            if (InputEnable())
                return Input.IsButtonPressDown(id);
            return false;
        }
        
        public bool WasButtonReleased(int id)
        {
            if (InputEnable())
                return Input.WasButtonReleased(id);
            return false;
        }

        private bool InputEnable()
        {
            return Enabled && Input != null;
        }
    }
}