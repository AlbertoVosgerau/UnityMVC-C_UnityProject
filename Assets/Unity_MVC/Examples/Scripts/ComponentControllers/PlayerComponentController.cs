using System;
using UnityEngine;
using UnityMVC;
using Object = UnityEngine.Object;

public class PlayerComponentController : ComponentController
{
    private PlayerComponent _component;
    
    // Start your code here
    public Action onGotTheBall;
    public void SetComponent(PlayerComponent component)
    {
        _component = component;
    }

    public override void OnComponentUpdate()
    {
        base.OnComponentUpdate();
    }

    public void OnBallHit(Collision ball)
    {
        Object.Destroy(ball.gameObject);
        onGotTheBall.Invoke();
    }
}