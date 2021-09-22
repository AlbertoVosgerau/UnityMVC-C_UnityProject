using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityMVC.Component;

namespace UnityMVC.Editor
{

    public class MVCDataDependencies
    {
        public MVCInspectorData controllers;
        public MVCInspectorData views;
        public MVCInspectorData mvcComponents;
        public MVCInspectorData mvcComponentGroups;
        public MVCInspectorData unityComponents;
    }

    public class MVCInspectorData
    {
        public int ItemsCount
        {
            get
            {
                int count = 0;
                foreach (MVCInspectorDataTypeResult result in results)
                {
                    count += result.dependenciesRoot.Count;
                }

                return count;
            }
        }
        
        public List<MVCInspectorDataTypeResult> results = new List<MVCInspectorDataTypeResult>();
    }

    public class MVCInspectorDataTypeResult
    {
        public Type type;
        public List<FieldInfo> dependenciesRoot;
    }
    public class MVCInspector
    {
        public static MVCDataDependencies GetDependenciesList(string nameSpaceFilter)
        {
            MVCDataDependencies data = new MVCDataDependencies();

            data.controllers = GetDependencies(typeof(Controller.Controller), nameSpaceFilter);
            data.views = GetDependencies(typeof(View.View), nameSpaceFilter);
            data.unityComponents = GetDependencies(typeof(UnityComponent.UnityComponent), nameSpaceFilter);
            data.mvcComponentGroups = GetDependencies(typeof(MVCComponentGroup), nameSpaceFilter);

            var mvcComponents = GetDependencies(typeof(MVCComponent), nameSpaceFilter);
            mvcComponents.results = mvcComponents.results.Where(x => x.type.ToString() == typeof(MVCComponentGroup).ToString()).ToList();
            data.mvcComponents = mvcComponents;

            return data;
        }
        
        public static MVCInspectorData GetDependencies(Type requiredType, string nameSpaceFilter)
        {
            MVCInspectorData data = new MVCInspectorData();

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
                result.dependenciesRoot = GetFields(type, requiredType);
                data.results.Add(result);
            }
            return data;
        }

        private static List<FieldInfo> GetFields(Type target, Type srcType)
        {
            List<FieldInfo> dependencies = new List<FieldInfo>();
            List<Type> types = MVCReflectionUtil.GetTypes(srcType);

            List<Type> filteredTypes = types.Where(x =>
                x.IsClass &&
                !x.IsAbstract &&
                !x.Name.Contains("Template") &&
                x.Namespace != target.Namespace &&
                !(x.Name.Contains($"{target.Name}Events") && x.Namespace.Contains($"MVC.Events")) &&
                !(x.Name.Contains($"{target.Name}Model") && x.Namespace.Contains($"MVC.Model")) &&
                x != target).ToList();

            List<FieldInfo> fields = target.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic).ToList();

            foreach (FieldInfo field in fields)
            {
                if (filteredTypes.Contains(field.FieldType))
                {
                    if (IsMVC(field.FieldType))
                    {
                        dependencies.Add(field);
                    }
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