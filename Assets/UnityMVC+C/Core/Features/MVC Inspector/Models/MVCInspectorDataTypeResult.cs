using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityMVC.Component;

namespace UnityMVC.Inspector
{
    public class MVCInspectorDataTypeResult
    {
        public Type type;
        public List<FieldInfo> fieldInfos = new List<FieldInfo>();

        public List<bool> IsOk
        {
            get
            {
                _isOk.Clear();
                foreach (FieldInfo info in fieldInfos)
                {
                    bool isSameNamespace = info.FieldType.Namespace == type.Namespace;
                    bool isController = info.FieldType.BaseType == typeof(Controller.Controller) &&
                                            type.BaseType == typeof(Controller.Controller);

                    bool isOk = isSameNamespace || isController;
                    _isOk.Add(isOk);
                }

                return _isOk;
            }
        }
        
        public List<MessageType> MessageTypes
        {
            get
            {
                _messageTypes.Clear();
                foreach (bool isOk in IsOk)
                {
                    MessageType messageType = isOk ? MessageType.Info : MessageType.Warning;
                    _messageTypes.Add(messageType);
                }

                return _messageTypes;
            }
        }

        private List<MessageType> _messageTypes = new List<MessageType>();

        private List<bool> _isOk = new List<bool>();
    }
}