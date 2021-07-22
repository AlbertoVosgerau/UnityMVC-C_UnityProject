using System;
using System.IO;
using System.Net;
using UnityEngine;

namespace UnityMVC
{
    public class APIHelper
    {
        public static void APIGet<T>(string url, Action<T> callback)
        {
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            myReq.BeginGetResponse(new AsyncCallback(delegate (IAsyncResult asyncResult)
            {
                FinishWebRequest(asyncResult, callback);
            }), myReq);
        }

        private static void FinishWebRequest<T>(IAsyncResult ar, Action<T> callback)
        {
            HttpWebResponse response = (ar.AsyncState as HttpWebRequest).EndGetResponse(ar) as HttpWebResponse;

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var objText = reader.ReadToEnd();
                callback.Invoke(JsonUtility.FromJson<T>(objText));
            }
        }
    }
}