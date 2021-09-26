using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityMVC.Component;

namespace UnityMVC.Inspector
{
    public class MVCInspector
    {
        public static MVCDependencyResult GetDependenciesList(string nameSpaceFilter)
        {
            MVCDependencyResult data = new MVCDependencyResult();

            data.controllers.SetResults(GetDependencies(typeof(Controller.Controller), nameSpaceFilter));
            data.views.SetResults(GetDependencies(typeof(View.View), nameSpaceFilter));
            data.unityComponents.SetResults(GetDependencies(typeof(UnityComponent.UnityComponent), nameSpaceFilter));
            data.mvcComponentGroups.SetResults(GetDependencies(typeof(UnityComponent.UnityComponent), nameSpaceFilter));

            var mvcComponents = GetDependencies(typeof(MVCComponent), nameSpaceFilter);
            var tempResults = mvcComponents.Where(x => x.type.ToString() == typeof(MVCComponentGroup).ToString()).ToList();
            data.mvcComponents.SetResults(tempResults);

            return data;
        }

        public static List<MVCInspectorDataTypeResult> GetDependencies(Type requiredType, string nameSpaceFilter)
        {
            List<MVCInspectorDataTypeResult> results = new List<MVCInspectorDataTypeResult>();

            List<Type> types = MVCReflectionUtil.GetTypes(requiredType);

            List<Type> filteredTypes = types.Where(x =>
                x.IsClass &&
                !x.IsAbstract &&
                x.IsSubclassOf(requiredType) &&
                x.Namespace == nameSpaceFilter &&
                !x.Name.Contains("Template")).ToList();

            foreach (Type type in filteredTypes)
            {
                MVCInspectorDataTypeResult result = new MVCInspectorDataTypeResult();
                result.type = type;
                result.fieldInfos = GetFields(type, requiredType);
                results.Add(result);
            }
            
            return results;
        }

        private static List<FieldInfo> GetFields(Type target, Type srcType)
        {
            List<FieldInfo> dependencies = new List<FieldInfo>();

            List<FieldInfo> fields = target.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).ToList();

            foreach (FieldInfo field in fields)
            {
                if (IsMVC(field.FieldType))
                {
                    dependencies.Add(field);
                }
            }

            return dependencies;
        }

        private static bool IsMVC(Type type)
        {
            bool isController = type.BaseType == typeof(Controller.Controller);
            bool isView = type.BaseType == typeof(View.View);
            bool isMvcComponent = type.BaseType == typeof(MVCComponent);
            bool isUnityComponent = type.BaseType == typeof(UnityComponent.UnityComponent);

            return isController || isView || isMvcComponent || isUnityComponent;
        }
    }
}