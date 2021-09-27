#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace UnityMVC.Inspector
{

    public class MVCInspectorData<T> where T : class
    {
        public int ItemsCount
        {
            get
            {
                int count = 0;
                foreach (MVCInspectorDataTypeResult result in Results)
                {
                    count += result.fieldInfos.Count;
                }

                return count;
            }
        }

        public bool IsOk
        {
            get
            {
                bool isOk = true;
                foreach (MVCInspectorDataTypeResult result in _results)
                {
                    foreach (bool b in result.IsOk)
                    {
                        if (!b)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public List<MVCInspectorDataTypeResult> Results => _results;
        private List<MVCInspectorDataTypeResult> _results = new List<MVCInspectorDataTypeResult>();

        public void ClearResult()
        {
            _results.Clear();
        }
        public void SetResults(List<MVCInspectorDataTypeResult> results)
        {
            _results = results;
        }
    }
}
#endif