using System.Collections;
using UnityEngine;
using UnityMVC;
public class CounterController : Controller
{
    private bool _keepCouting = true;
    public override void OnViewStart()
    {
        base.OnViewStart();
        Debug.Log("Start counting...");
        CoroutineHelper.StartCoroutine(CountTime(0));
    }

    public override void OnViewDestroy()
    {
        base.OnViewDestroy();
    }

    private IEnumerator CountTime(float initialTime)
    {
        float time = initialTime;
        while (time < 1000 && _keepCouting)
        {
            time += Time.deltaTime;
            Debug.Log($"Time: {time}");
            yield return null;
        }
    }
}
