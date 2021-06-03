using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityMVC;
public class UnityMVCConterController : Controller
{
    private int odd = 1;
    private int even = 0;
    public override void OnViewStart()
    {
        base.OnViewStart();   
        CoroutineHelper.StartCoroutine(this, CountOdd());
        CoroutineHelper.StartCoroutine(this, CountEven());
    }

    public override void OnViewDestroy()
    {
        base.OnViewDestroy();
    }

    private IEnumerator CountOdd()
    {
        yield return new WaitForSeconds(1);
        while (odd < 1000)
        {
            odd += 2;
            Debug.Log($"Odd count: {odd}");
        }
    }
    
    private IEnumerator CountEven()
    {
        yield return new WaitForSeconds(1);
        while (even < 1000)
        {
            even += 2;
            Debug.Log($"Even count: {even}");
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
