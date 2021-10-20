using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityMVC.Model
{
    [Serializable]
    public class UnityMVCModuleModel 
    {
        public string moduleName;
        public string moduleNamespace;
        public string relativePath;

        public UnityMVCModuleModel(string moduleName, string moduleNamespace)
        {
            this.moduleName = moduleName;
            this.moduleNamespace = moduleNamespace;
        }
    }
}