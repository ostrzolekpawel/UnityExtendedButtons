using UnityEngine;
using Input = ExtendedButtons.CustomInput.Input;

namespace ExtendedButtons
{
    public class ButtonsListenerCustomInput
    {
        protected readonly Camera button3DCamera;
        protected readonly Input input;
        protected readonly float moveTrashHold = 4.0f;
        protected readonly float maxDistance = 100.0f;
        protected readonly LayerMask layerMask = 1;

        protected Button3D followedButton3D = null;
        protected Vector3 firstInputPosition;
        protected bool moved = false;

        /// <summary>
        /// button3D is locked when pointer is down
        /// </summary>
        protected Button3D buttonLocked;

        #region Constructors
        public ButtonsListenerCustomInput(Input input)
        {
            this.input = input;
            button3DCamera = Camera.main;
        }

        public ButtonsListenerCustomInput(Input input, Camera camera)
        {
            this.input = input;
            button3DCamera = camera;
        }

        public ButtonsListenerCustomInput(Input input, Camera camera, float maxDistance)
        {
            this.input = input;
            button3DCamera = camera;
            this.maxDistance = maxDistance;
        }

        public ButtonsListenerCustomInput(Input input, Camera camera, float maxDistance, LayerMask layerMask)
        {
            this.input = input;
            button3DCamera = camera;
            this.maxDistance = maxDistance;
            this.layerMask = layerMask;
        } 
        #endregion

        public virtual void Tick()
        {
            if (input != null)
                Listener();
        }
        
        protected virtual void Listener()
        {
            if (input.WasButtonReleased(0) && buttonLocked != null)
            {
                buttonLocked.onUp?.Invoke();
                if (!moved)
                    buttonLocked.onClick?.Invoke();
                buttonLocked = null;
            }

            if (input.IsPointerOverGameObject) return;

            Ray ray = input.GetRay(button3DCamera);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask))
            {
                Button3D button = hit.transform.GetComponent<Button3D>();
                if (button != null)
                {
                    if (followedButton3D != button)
                    {
                        followedButton3D?.onExit?.Invoke();
                        followedButton3D = button;
                        followedButton3D?.onEnter?.Invoke();
                    }

                    if (input.WasButtonPressed(0))
                    {
                        moved = false;
                        firstInputPosition = input.Position;
                        button.onDown?.Invoke();
                        buttonLocked = button;
                    }

                    // button is pressed on Button3D, check if cursor is moved and cancel possibility to onClick if necessary
                    if (input.IsButtonPressDown(0))
                    {
                        float movedDistance = Mathf.Abs(Vector3.Distance(firstInputPosition, input.Position));
                        if (!moved && movedDistance > moveTrashHold)
                            moved = true;
                    }
                }
                else
                {
                    followedButton3D?.onExit?.Invoke();
                    followedButton3D = null;
                }
            }
            else
            {
                followedButton3D?.onExit?.Invoke();
                followedButton3D = null;
            }
        }
    }
}
