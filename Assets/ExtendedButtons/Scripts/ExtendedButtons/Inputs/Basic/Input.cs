using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ExtendedButtons.CustomInput
{
    public abstract class Input
    {
        protected float _doubleTapDelay = 0.3f;
        protected float _lastTapTime = 0.0f;
        protected float _doubleTapTrashHold = 12f;
        private Vector3 _lastClickPosition;
        public event Action OnUpdate;

        private Camera _camera;
        public virtual Camera Camera
        {
            get {
                if (_camera == null)
                    _camera = Camera.main;
                return _camera;
            }
        }

        public virtual bool IsPointerOverGameObject { get; }
        public Vector3 Position { get; protected set; }
        public Vector3 DeltaPosition { get; protected set; }
        protected Vector3 lastPosition;

        public abstract Ray GetRay(Camera camera);
        public abstract Ray GetGraphicRay(PointerEventData pointerData, Camera camera = null);

        public abstract bool WasMoved { get; }
        public abstract bool HasPointer { get; }
        public bool DoubleTap { get; set; }

        public abstract bool PinchAvaible { get; }
        public abstract float Pinch { get; }

        protected virtual void DetectDoubleTap()
        {
            if (DoubleTap) DoubleTap = false; // clear in next frame
            if (WasButtonPressed(0))
            {
                if ((Time.time - _lastTapTime) < _doubleTapDelay && (Position - _lastClickPosition).sqrMagnitude < _doubleTapTrashHold * _doubleTapTrashHold)
                {
                    _lastTapTime = 0.0f;

                    DoubleTap = true;
                    Debug.Log("Double click handler hohoo");
                }
                else
                {
                    DoubleTap = false;
                    _lastTapTime = Time.time;
                    _lastClickPosition = Position;
                }
            }
        }

        /// <summary>
        /// Ticks this instance.
        /// </summary>
        public virtual void Tick()
        {
            OnUpdate?.Invoke();
        }

        /// <summary>
        /// button was pressed
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract bool WasButtonPressed(int id);

        /// <summary>
        /// button is held down
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract bool IsButtonPressDown(int id);

        /// <summary>
        /// button is released
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract bool WasButtonReleased(int id);

        /// <summary>
        /// Gets the state was pressed.
        /// </summary>
        /// <param name="id">Input type.</param>
        /// <returns></returns>
        protected abstract bool GetStateWasPressed(int id);

        /// <summary>
        /// Gets the state is press down.
        /// </summary>
        /// <param name="id">Input type.</param>
        /// <returns></returns>
        protected abstract bool GetStateIsPressDown(int id);

        /// <summary>
        /// Gets the state was released.
        /// </summary>
        /// <param name="id">Input type.</param>
        /// <returns></returns>
        protected abstract bool GetStateWasReleased(int id);
        
        public Input() { }

        public Input(Action _updateAction)
        {
            OnUpdate += _updateAction;
        }

        public Input(Action _updateAction, BaseInputModule _inputModule) { }
    }
}