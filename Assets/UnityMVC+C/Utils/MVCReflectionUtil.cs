using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class MVCReflectionUtil
{
    private static List<Assembly> _assemblies = new List<Assembly>();
    private static List<Type> _types = new List<Type>();

    public static void UpdateData()
    {
        UpdateAssemblies();
        UpdateTypes();
    }

    private static void UpdateAssemblies()
    {
        _assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
    }

    private static void UpdateTypes()
    {
        _types = new List<Type>();

        foreach (var assembly in _assemblies)
        {
            var internalTypes = assembly.GetTypes();
            foreach (Type type in internalTypes)
            {
                _types.Add(type);
            }
        }
    }

    public static List<Assembly> GetAllAssembies()
    {
        if (_assemblies.Count == 0)
        {
            UpdateData();
        }
        return _assemblies;
    }
    
    public static List<Type> GetAllTypes()
    {
        if (_assemblies.Count == 0)
        {
            UpdateData();
        }
        return _types;
    }
    
    public static List<Type> GetTypes(Type type)
    {
        if (_assemblies.Count == 0)
        {
            UpdateData();
        }
        List<Type> filteredTypes = _types.Where(x =>
            x.IsClass &&
            !x.IsAbstract &&
            x.IsSubclassOf(type) &&
            !x.Name.Contains("Template")).ToList();

        return filteredTypes;
    }
    
    public static List<Type> GetTypes(Type type, string namespaceFilter)
    {
        if (_assemblies.Count == 0)
        {
            UpdateData();
        }
        List<Type> filteredTypes = _types.Where(x =>
            x.IsClass &&
            !x.IsAbstract &&
            x.IsSubclassOf(type) &&
            !x.Name.Contains("Template") &&
            x.Namespace.Contains(namespaceFilter)).ToList();

        return filteredTypes;
    }
}
