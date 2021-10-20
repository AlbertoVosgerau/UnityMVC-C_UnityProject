using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityMVC.Model
{
    public class UnityMVCApplicationModel
    {
        public string applicationName;
        public string companyName;

        public UnityMVCApplicationModel(string applicationName, string companyName)
        {
            this.applicationName = applicationName;
            this.companyName = companyName;
        }
    }
}