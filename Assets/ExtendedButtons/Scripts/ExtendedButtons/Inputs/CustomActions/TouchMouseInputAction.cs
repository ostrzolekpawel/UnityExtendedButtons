using ExtendedButtons.CustomInput;
using System.Collections;
using UnityEngine;

namespace ExtendedButtons.Example
{
    /// <summary>
    /// example class for InputSystem usage
    /// </summary>
    public class TouchMouseInputAction : MonoBehaviour
    {
        private const float PLAYER_HEIGHT = 1.33f;
        [SerializeField] private Camera playerCamera;
        private readonly ClampRotate clampRotate = new ClampRotate();

        private Vector3 positionTeleport;
        private Vector3 positionTeleportFrom;
        private bool isTeleporting = false;
        private readonly float teleportTime = 0.5f;
        private float timer = 0.0f;

        private void Start()
        {
            InputSystem.ForceChangeInput(new MouseInput(Tick));
            StartCoroutine(DetectInputType());
        }

        /// <summary>
        /// switch input when touch or mouse detect
        /// </summary>
        /// <returns></returns>
        private IEnumerator DetectInputType()
        {
            UnityEngine.Input.simulateMouseWithTouches = false;
            while (true)
            {
                if (UnityEngine.Input.GetMouseButton(0) || UnityEngine.Input.GetMouseButton(1) || UnityEngine.Input.GetMouseButton(2))
                    InputSystem.Input = new MouseInput(Tick);
                if (UnityEngine.Input.touchCount > 0)
                    InputSystem.Input = new TouchInput(Tick);
                yield return null;
            }
        }

        private void Tick()
        {
            if (InputSystem.Input.DoubleTap && !InputSystem.Input.IsPointerOverGameObject)
            {
                Ray ray = InputSystem.Input.GetRay(playerCamera);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Vector3 pos = hit.point + hit.normal * PLAYER_HEIGHT;
                    positionTeleport = pos;
                    isTeleporting = true;
                    positionTeleportFrom = playerCamera.transform.position;
                }
            }

            if (isTeleporting)
            {
                timer += Time.deltaTime;
                playerCamera.transform.position = Vector3.Lerp(positionTeleportFrom, positionTeleport, timer / teleportTime); //.0466666f
                if (Vector3.Distance(playerCamera.transform.position, positionTeleport) < 0.01f)
                {
                    isTeleporting = false;
                    timer = 0.0f;
                }
            }

            if (!InputSystem.Input.IsPointerOverGameObject)
            {
                if (InputSystem.Input.IsButtonPressDown(0))
                {
                    playerCamera.transform.localEulerAngles = clampRotate.Rotate(playerCamera.transform);
                }

                if (InputSystem.Input.PinchAvaible)
                {
                    positionTeleport = playerCamera.transform.position + playerCamera.transform.forward * InputSystem.Input.Pinch * 0.75f;
                    positionTeleportFrom = playerCamera.transform.position;
                    isTeleporting = true;
                }
            }
        }
    }

    public class ClampRotate
    {
        private readonly float sensitivityX = 0.05f;
        private readonly float sensitivityY = 0.05f;

        private readonly float minimumX = -360.0f;
        private readonly float maximumX = 360.0f;

        private readonly float minimumY = -60.0f;
        private readonly float maximumY = 60.0f;

        private float rotationY = 0.0f;

        public ClampRotate() { }

        public ClampRotate(float sensitivityX, float sensitivityY, float minimumX, float maximumX, float minimumY, float maximumY)
        {
            this.sensitivityX = sensitivityX;
            this.sensitivityY = sensitivityY;
            this.minimumX = minimumX;
            this.maximumX = maximumX;
            this.minimumY = minimumY;
            this.maximumY = maximumY;
        }

        public Vector3 Rotate(Transform trans)
        {
            float rotationX = trans.transform.localEulerAngles.y - InputSystem.Input.DeltaPosition.x * sensitivityX;
            rotationY += InputSystem.Input.DeltaPosition.y * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            return new Vector3(rotationY, rotationX, 0);
        }
    }
}