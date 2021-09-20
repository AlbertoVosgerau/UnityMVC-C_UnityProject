using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityMVC.Component;

namespace UnityMVC.Editor
{

    public class MVCInspectorData
    {
        public List<MVCInspectorDataTypeResult> results = new List<MVCInspectorDataTypeResult>();
    }

    public class MVCInspectorDataTypeResult
    {
        public Type type;
        public List<FieldInfo> dependenciesRoot;
    }
    public class MVCInspector
    {
        public static MVCInspectorData GetDependencies(Type requiredType)
        {
            MVCInspectorData data = new MVCInspectorData();

            List<Type> controllers = Assembly.GetAssembly(requiredType).GetTypes().ToList();
            
            List<Type> filteredTypes = controllers.Where(x =>
                x.IsClass &&
                !x.IsAbstract &&
                x.IsSubclassOf(requiredType) &&
                !x.Name.Contains("Template")).ToList();

            foreach (Type type in filteredTypes)
            {
                MVCInspectorDataTypeResult result = new MVCInspectorDataTypeResult();
                result.type = type;
                result.dependenciesRoot = GetControllerDependencies(type);
                data.results.Add(result);
            }

            return data;
        }
        
        

        public static List<FieldInfo> GetControllerDependencies(Type target)
        {
            return GetFields(target, typeof(Controller.Controller));
        }
    
        private static List<FieldInfo> GetComponentGroupDependencies(Type target)
        {
            return GetFields(target, typeof(MVCComponentGroup));
        }
        
        private static List<FieldInfo> GetComponentDependencies(Type target)
        {
            return GetFields(target, typeof(MVCComponentGroup));
        }
        private static List<FieldInfo> GetViewDependencies(Type target)
        {
            return GetFields(target, typeof(MVCComponentGroup));
        }
        private static List<FieldInfo> GetUnityComponentDependencies(Type target)
        {
            return GetFields(target, typeof(MVCComponentGroup));
        }

        private static List<FieldInfo> GetFields(Type target, Type srcType)
        {
            List<FieldInfo> dependencies = new List<FieldInfo>();
            List<Type> types = Assembly.GetAssembly(srcType).GetTypes().ToList();


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
                    dependencies.Add(field);
                }
            }

            return dependencies;
        }
    }
}