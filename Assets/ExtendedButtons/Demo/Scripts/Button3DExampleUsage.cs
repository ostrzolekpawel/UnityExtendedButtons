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
                Debug.Log("OnButton Enter");
                meshRenderer.material = red;
            });
            button?.onDown.AddListener(() =>
            {
                Debug.Log("OnButton Down");
                child.SetActive(true);
            });
            button?.onClick.AddListener(() =>
            {
                Debug.Log("OnButton Click");
            });
            button?.onUp.AddListener(() =>
            {
                Debug.Log("OnButton Up");
                child.SetActive(false);
            });
            button?.onExit.AddListener(() =>
            {
                Debug.Log("OnButton Exit");
                meshRenderer.material = white;
            });
        }
    }
}