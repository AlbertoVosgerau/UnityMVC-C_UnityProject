using UnityEngine;
using UnityMVC;
public class BallComponentController : ComponentController
{
    private BallComponent _component;
    
    // Start your code here
    public void SetComponent(BallComponent component)
    {
        _component = component;
    }

    public override void OnComponentUpdate()
    {
        base.OnComponentUpdate();
        if (IsAlive())
        {
            return;
        }
        SelfDestroy();
    }

    public bool IsAlive()
    {
        return _component.transform.position.y > -1;
    }

    public void SelfDestroy()
    {
        Object.Destroy(_component.gameObject);
    }
}