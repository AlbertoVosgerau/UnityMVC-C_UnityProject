using UnityEngine;

namespace UnityMVC
{
    public class CoroutineHelperComponent : MonoBehaviour
    {
        public static CoroutineHelperComponent Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject newObject = new GameObject("CoroutineHelper");
                    _instance = newObject.AddComponent<CoroutineHelperComponent>();
                    DontDestroyOnLoad(newObject);
                }
                return _instance;
            }
        }
        private static CoroutineHelperComponent _instance;

        private void OnDestroy()
        {
            CoroutineHelper.StopAllCoroutines();
        }
    }
}