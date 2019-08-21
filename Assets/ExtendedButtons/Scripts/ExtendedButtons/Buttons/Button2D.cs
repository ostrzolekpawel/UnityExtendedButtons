using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExtendedButtons
{
    [RequireComponent(typeof(ICanvasElement))]
    public class Button2D : Selectable, IPointerClickHandler, IEventSystemHandler
    {
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

            if (onEnter == null)
                onEnter = new ButtonEvent();
            if (onDown == null)
                onDown = new ButtonEvent();
            if (onClick == null)
                onClick = new ButtonEvent();
            if (onUp == null)
                onUp = new ButtonEvent();
            if (onExit == null)
                onExit = new ButtonEvent();
        }
        
        protected override void Awake()
        {
            Init();
        }

        protected Button2D() { }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable) return;
            onClick?.Invoke();
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            onDown?.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            onUp?.Invoke();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            onEnter?.Invoke();
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            onExit?.Invoke();
        }

        [Serializable]
        public class ButtonEvent : UnityEvent
        {
            public ButtonEvent() { }
        }
    }
}
