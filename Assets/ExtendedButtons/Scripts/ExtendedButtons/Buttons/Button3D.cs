using UnityEngine;
using UnityEngine.Events;

namespace ExtendedButtons
{
    [RequireComponent(typeof(Collider))]
    public class Button3D : MonoBehaviour
    {
        public bool Interactable { get; set; } = true;
        public ButtonEvent onEnter;
        public ButtonEvent onDown;
        public ButtonEvent onUp;
        public ButtonEvent onClick;
        public ButtonEvent onExit;
        protected bool isInit = false;


        public void Init()
        {
            if (isInit) return;
            isInit = true;

            if (onClick == null)
                onClick = new ButtonEvent();
            if (onDown == null)
                onDown = new ButtonEvent();
            if (onUp == null)
                onUp = new ButtonEvent();
            if (onEnter == null)
                onEnter = new ButtonEvent();
            if (onExit == null)
                onExit = new ButtonEvent();
        }

        protected void Awake()
        {
            Init();
        }

        [System.Serializable]
        public class ButtonEvent : UnityEvent
        {
            public ButtonEvent() { }
        }
    }
}
