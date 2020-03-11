using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExtendedButtons
{
    [RequireComponent(typeof(ICanvasElement))]
    public class Button2D : Button, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public UnityEvent onEnter;
        public UnityEvent onDown; 
        public UnityEvent onUp;
        public UnityEvent onExit;

        public UnityEvent onBeginDrag;
        public UnityEvent onDrag;
        public UnityEvent onEndDrag;

        protected bool isInit = false;

        public void Init()
        {
            if (isInit) return;
            isInit = true;

            if (onEnter == null)
                onEnter = new UnityEvent();
            if (onDown == null)
                onDown = new UnityEvent();
            if (onUp == null)
                onUp = new UnityEvent();
            if (onExit == null)
                onExit = new UnityEvent();

            if (onBeginDrag == null)
                onBeginDrag = new UnityEvent();
            if (onDrag == null)
                onDrag = new UnityEvent();
            if (onEndDrag == null)
                onEndDrag = new UnityEvent();
        }
        
        protected override void Awake()
        {
            Init();
        }

        protected Button2D() { }
        

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

        public void OnBeginDrag(PointerEventData eventData)
        {
            onBeginDrag?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            onDrag?.Invoke();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            onEndDrag?.Invoke();
        }
    }
}
