using UnityEngine;

namespace UnityMVC
{
    public class Component : MonoBehaviour
    {
        protected View _view;
    
        public void SetView(View view)
        {
            _view = view;
        }
    }
}