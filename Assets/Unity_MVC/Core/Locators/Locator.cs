using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityMVC
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
            return new U();
        }
    
        public bool Has<U>() where U : class, T, new()
        {
            if (_dictionary == null)
            {
                Initialize();
            }
            return _dictionary.ContainsKey(typeof(U));
        }
    
        public U Get<U>() where U : class, T, new()
        {
            if (!Has<U>())
            {
                _dictionary[typeof(U)] = Create<U>();
                return _dictionary[typeof(U)] as U;
            }
            return _dictionary[typeof(U)] as U;
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
