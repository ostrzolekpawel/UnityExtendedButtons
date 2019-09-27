using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ExtendedButtons.CustomInput
{
    public class TouchInput : Input
    {
        private readonly int maxNumberOfTouches = 2;

        public override bool IsPointerOverGameObject
        {
            get => EventSystem.current.IsPointerOverGameObject();
        }

        public override Ray GetRay(Camera camera)
        {
            Ray ray = new Ray(Vector3.zero, Vector3.zero);
            if (UnityEngine.Input.touchCount != 0)
            {
                Touch touch = UnityEngine.Input.GetTouch(0);
                ray = camera.ScreenPointToRay(touch.position);
            }
            return ray;
        }

        public override Ray GetGraphicRay(PointerEventData pointerData, Camera camera)
        {
            return camera.ScreenPointToRay(pointerData.position);
        }

        public override bool WasMoved
        {
            get {
                int touchCount = UnityEngine.Input.touchCount;
                if (touchCount != 0)
                {
                    for (int touchIndex = 0; touchIndex < touchCount; ++touchIndex)
                    {
                        if (touchIndex >= maxNumberOfTouches) return false;

                        Touch touch = UnityEngine.Input.GetTouch(touchIndex);
                        if (touch.phase == TouchPhase.Moved) return true;
                    }
                }
                return false;
            }
        }

        public override bool HasPointer
        {
            get => UnityEngine.Input.touchCount != 0;
        }

        public override bool PinchAvaible
        {
            get => (UnityEngine.Input.touchCount >= 2);
        }

        /// <summary>
        /// Find the difference in the distances between delta of two touches
        /// </summary>
        public override float Pinch
        {
            get {
                Touch touchZero = UnityEngine.Input.GetTouch(0);
                Touch touchOne = UnityEngine.Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                return Mathf.Clamp(-deltaMagnitudeDiff, -0.1f, 0.1f);
            }
        }

        protected override void DetectDoubleTap()
        {
            if (DoubleTap) DoubleTap = false; // clear in next frame
            if (WasButtonPressed(0))
            {
                if ((Time.time - _lastTapTime) < _doubleTapDelay)
                {
                    _lastTapTime = 0.0f;

                    DoubleTap = true;
                    Debug.Log("Double click handler hohoo");
                }
                else
                {
                    DoubleTap = false;
                    _lastTapTime = Time.time;
                }
            }
        }

        public override void Tick()
        {
            base.Tick();

            if (UnityEngine.Input.touchCount != 0)
            {
                Position = UnityEngine.Input.GetTouch(0).position;
                DeltaPosition = UnityEngine.Input.GetTouch(0).deltaPosition;
            }
            else
                DeltaPosition = Vector3.zero;

            DetectDoubleTap();
        }

        public override bool WasButtonPressed(int id)
        {
            return GetStateWasPressed(id);
        }

        public override bool IsButtonPressDown(int id)
        {
            return GetStateIsPressDown(id);
        }
        
        public override bool WasButtonReleased(int id)
        {
            return GetStateWasReleased(id);
        }

        protected override bool GetStateWasPressed(int id)
        {
            int touchCount = UnityEngine.Input.touchCount;
            if (id >= touchCount || touchCount > maxNumberOfTouches || PinchAvaible)
                return false;
            return UnityEngine.Input.GetTouch(id).phase == TouchPhase.Began;
        }

        protected override bool GetStateIsPressDown(int id)
        {
            int touchCount = UnityEngine.Input.touchCount;
            if (id >= touchCount || touchCount > maxNumberOfTouches || PinchAvaible)
                return false;
            return true;
        }

        protected override bool GetStateWasReleased(int id)
        {
            int touchCount = UnityEngine.Input.touchCount;
            if (id >= touchCount || touchCount > maxNumberOfTouches || PinchAvaible)
                return false;

            Touch touch = UnityEngine.Input.GetTouch(id);
            return touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
        }

        public TouchInput() { }
        public TouchInput(Action _updateAction) : base(_updateAction) { }
    }
}
