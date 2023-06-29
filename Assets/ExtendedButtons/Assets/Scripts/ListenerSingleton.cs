using UnityEngine;

namespace ExtendedButtons
{
    public class ListenerSingleton : MonoBehaviour
    {
        private static IButtonsListener _instance;

        [RuntimeInitializeOnLoadMethod]
        private static void CreateInstance()
        {
            if (_instance == null)
            {
                _instance = (IButtonsListener)FindObjectOfType(typeof(ButtonsListenerMono));
                if (_instance == null)
                {
                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<ButtonsListenerBasic>();
                    singletonObject.name = typeof(ButtonsListenerBasic).ToString() + " (Singleton)";
                }
            }
        }
    }
}