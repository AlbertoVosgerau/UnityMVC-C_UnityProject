using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityMVC
{
    public class UnityController
    {
        private UnityView _view;

        public void SetView(UnityView view)
        {
            _view = view;
        }

        public virtual void OnViewStart()
        {
        
        }

        public virtual void OnViewDestroy()
        {
        
        }
    }
}