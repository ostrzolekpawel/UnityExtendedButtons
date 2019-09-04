using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ExtendedButtons.CustomInput
{
    public class MouseInput : Input
    {
        public override bool IsPointerOverGameObject
        {
            get => EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }

        public override Ray GetRay(Camera camera)
        {
            if (camera == null)
                return new Ray();
            return camera.ScreenPointToRay(Position);
        }

        public override Ray GetGraphicRay(PointerEventData pointerData, Camera camera)
        {
            return camera.ScreenPointToRay(pointerData.position);
        }

        public override bool WasMoved
        {
            get => UnityEngine.Input.GetAxis("Mouse X") != 0.0f || UnityEngine.Input.GetAxis("Mouse Y") != 0.0f;
        }

        public override bool HasPointer
        {
            get => UnityEngine.Input.mousePresent;
        }

        public override bool PinchAvaible
        {
            get => (Mathf.Abs(UnityEngine.Input.GetAxis("Mouse ScrollWheel")) > 0.0f);
        }

        public override float Pinch
        {
            get => UnityEngine.Input.GetAxis("Mouse ScrollWheel");
        }

        public override void Tick()
        {
            base.Tick();

            Position = UnityEngine.Input.mousePosition;
            DeltaPosition = Position - lastPosition;
            lastPosition = Position;

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
            return UnityEngine.Input.GetMouseButtonDown(id);
        }

        protected override bool GetStateIsPressDown(int id)
        {
            return UnityEngine.Input.GetMouseButton(id);
        }

        protected override bool GetStateWasReleased(int id)
        {
            return UnityEngine.Input.GetMouseButtonUp(id);
        }

        public MouseInput() { }
        public MouseInput(Action _updateAction) : base(_updateAction) { }
    }
}
