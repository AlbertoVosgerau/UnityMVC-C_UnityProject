using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityMVC.Locator
{
    public class Locator<T> where T: class
    {
        protected Dictionary<Type, T> _dictionary;
        protected void Initialize()
        {
            _dictionary = new Dictionary<Type, T>();
        }
        protected U Create<U>() where U : class, T, new()
        {
            if (Has<U>())
            {
                Debug.LogException(new Exception("Controller already present"));
            }
            return new U();
        }
    
        public bool Has<U>() where U : class, T, new()
        {
            if (_dictionary == null)
            {
                Initialize();
            }
            return _dictionary.Keys.Any(x => x.IsAssignableFrom(typeof(U)) || x.IsSubclassOf(typeof(U)) || x is U);
        }
    
        public U Get<U>() where U : class, T, new()
        {
            if (!Has<U>())
            {
                _dictionary[typeof(U)] = Create<U>();
                return _dictionary[typeof(U)] as U;
            }

            try
            {
                return _dictionary[typeof(U)] as U;
            }
            catch (Exception e)
            {
                throw new Exception($"Controller {typeof(U)} already present. Message:\n{e.Message}");
            }
        }

        public void Remove(T t)
        {
            if (_dictionary == null)
            {
                return;
            }
            Type type = t.GetType();
            _dictionary.Remove(type);
        }

        public void Clear()
        {
            if (_dictionary == null)
            {
                return;
            }
            _dictionary.Clear();
        }
    }
}
