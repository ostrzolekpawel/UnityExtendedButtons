using UnityEngine;

namespace ExtendedButtons.Example
{
    public class Button3DExampleUsage : MonoBehaviour
    {
        [SerializeField] private Material red;
        [SerializeField] private Material white;
        [SerializeField] private GameObject child;

        private void Start()
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            var button = GetComponent<Button3D>();
            button?.onEnter.AddListener(() =>
            {
                Debug.Log($"OnButton Enter {gameObject.name}");
                meshRenderer.material = red;
            });
            button?.onDown.AddListener(() =>
            {
                Debug.Log($"OnButton Down {gameObject.name}");
                child.SetActive(true);
            });
            button?.onClick.AddListener(() =>
            {
                Debug.Log($"OnButton Click {gameObject.name}");
            });
            button?.onUp.AddListener(() =>
            {
                Debug.Log($"OnButton Up {gameObject.name}");
                child.SetActive(false);
            });
            button?.onExit.AddListener(() =>
            {
                Debug.Log($"OnButton Exit {gameObject.name}");
                meshRenderer.material = white;
            });

            button?.onBeginDrag.AddListener(() =>
            {
                Debug.Log($"OnButton BeginDrag {gameObject.name}");
            });
            button?.onDrag.AddListener(() =>
            {
                Debug.Log($"OnButton Drag {gameObject.name}");
            });
            button?.onEndDrag.AddListener(() =>
            {
                Debug.Log($"OnButton EndDrag {gameObject.name}");
            });
        }
    }
}