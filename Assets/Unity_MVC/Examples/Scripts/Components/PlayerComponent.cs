using System;
using UnityEngine;
using Component = UnityMVC.Component;

public class PlayerComponentEvents
{
    public Action onGotTheBall;
}

public class PlayerComponentInfo
{
    
}

public class PlayerComponent : Component
{
    public PlayerComponentInfo Info => _info;
    private PlayerComponentInfo _info = new PlayerComponentInfo();
    public PlayerComponentEvents Events => _events;
    private PlayerComponentEvents _events = new PlayerComponentEvents();
    
    // Start your code here
    
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void SolveDependencies()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        Events.onGotTheBall.Invoke();
    }
}